using Common.Module.Utils;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using KodyPocztowe.Module.BusinessObjects;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;

namespace PobierzWaluty
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pobieramy kursy walut!");

            var client = new RestClient("http://api.nbp.pl/api/exchangerates/tables/C/2019-09-01/2019-09-02/");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);

            Console.WriteLine(response.Content);



            var res = JsonConvert.DeserializeObject<List<Notowanie>>(response.Content);

            if (res != null)
            {
                using (XPObjectSpaceProvider directProvider = new XPObjectSpaceProvider(AppSettings.ConnectionString, null))
                {
                    using (IObjectSpace directObjectSpace = directProvider.CreateObjectSpace())
                    {
                        XafTypesInfo.Instance.RegisterEntity(typeof(KursWaluty));
                        XafTypesInfo.Instance.RegisterEntity(typeof(Waluta));
                        var wr = new WalutyReader(directObjectSpace);
                        wr.WczytajNotowania(res);
                        directObjectSpace.CommitChanges();

                    }
                }
            }
            Console.ReadLine();
        }
    }
}
