using CommonLibrary;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrajeWaluty.Module.Utils
{
   public static class KursyNBPHelper
    {

        public static void PobierzKursyNBP(int rok, string tabela = "C")
        {

            for (int i = 1; i < 13; i++)
            {
                var startDate = new DateTime(rok, i, 1);
                var endDate = startDate.GetLastDayOfMonth();

                IRestResponse response = PobierzKursyNBP(startDate, endDate, tabela);

                Console.WriteLine(response.Content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)

                {
                    var res = JsonConvert.DeserializeObject<List<Notowanie>>(response.Content);

                    WalutyHelper.WczytajKursy(res);
                }
            }

        }

        public static void PobierzKursyNBP(string tabela = "C")
        {
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-10);


            IRestResponse response = PobierzKursyNBP(startDate, endDate, tabela);

            Console.WriteLine(response.Content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)

            {
                var res = JsonConvert.DeserializeObject<List<Notowanie>>(response.Content);

                WalutyHelper.WczytajKursy(res);
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
    }
    public class Notowanie
    {
        public string table { get; set; }
        public string no { get; set; }
        public DateTime tradingDate { get; set; }
        public DateTime effectiveDate { get; set; }
        public Rate[] rates { get; set; }
    }

    public class Rate
    {
        public string currency { get; set; }
        public string code { get; set; }
        public decimal bid { get; set; }
        public decimal mid { get; set; }
        public decimal ask { get; set; }
    }
}
