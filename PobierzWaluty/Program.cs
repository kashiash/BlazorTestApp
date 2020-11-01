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



            //for (int i = 2002; i < 2021; i++)
            //{
            //    PobierzKursyNBP(i,"A");
            //    PobierzKursyNBP(i, "C");
            //}

            PobierzKursyNBP("A");
            PobierzKursyNBP( "C");

            Console.WriteLine("Zakonczono import wciśnij enter aby zakopńczyc program");
            Console.ReadLine();
        }

        private static void  PobierzKursyNBP(int rok ,string tabela = "C")
        {

            for (int i = 1; i < 13; i++)
            {
                var startDate = new DateTime(rok, i, 1);
                var endDate = startDate.GetLastDayOfMonth();

                IRestResponse response = PobierzKursyNBP(startDate, endDate,tabela);

                Console.WriteLine(response.Content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)

                {
                    var res = JsonConvert.DeserializeObject<List<Notowanie>>(response.Content);

                    WczytajKursy(res);
                }
            }

        }

        private static void PobierzKursyNBP(string tabela = "C")
        {
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-10);
            

                IRestResponse response = PobierzKursyNBP(startDate, endDate, tabela);

                Console.WriteLine(response.Content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)

                {
                    var res = JsonConvert.DeserializeObject<List<Notowanie>>(response.Content);

                    WczytajKursy(res);
                }
           

        }

        private static IRestResponse PobierzKursyNBP(DateTime starDate, DateTime endDate, string tabela = "C")
        {
            var url = $"http://api.nbp.pl/api/exchangerates/tables/{tabela}/{starDate.ToString("yyyy-MM-dd")}/{endDate.ToString("yyyy-MM-dd")}/";
            Console.WriteLine(url);
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);
            return response;
        }

        private static void WczytajKursy(List<Notowanie> res)
        {
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
        }
    }
    public static class DateTimeExt
    {
        public static DateTime GetLastDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
        }

    }
}
