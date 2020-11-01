using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace KodyPocztowe.Module.BusinessObjects
{
    public class WalutyReader

    {
        private IObjectSpace directObjectSpace;

        public WalutyReader(IObjectSpace directObjectSpace)
        {
            this.directObjectSpace = directObjectSpace;
        }

        public void WczytajNotowania(List<Notowanie> notowania)
        {
            foreach (var notowanie in notowania)
            {
                WczytajNotowanie(notowanie);

            }
        }

        private void WczytajNotowanie(Notowanie notowanie)
        {
            var effectiveDate = notowanie.effectiveDate;
            foreach (var kurs in notowanie.rates)
            {
                var waluta = directObjectSpace.GetObjectByKey<Waluta>(kurs.code);
                //  Guard.ArgumentNotNull(waluta, "Brak waluty");
                if (waluta != null)
                {

                    var kursWaluty = directObjectSpace.FindObject<KursWaluty>(CriteriaOperator.Parse("Waluta = ? And DataKursu = ?", waluta, effectiveDate));
                    if (kursWaluty == null)
                    {
                        kursWaluty = directObjectSpace.CreateObject<KursWaluty>();
                        kursWaluty.Waluta = waluta;
                        kursWaluty.DataKursu = effectiveDate;
                    }
                    if (kurs.bid > 0)
                        kursWaluty.KursSkupu = kurs.bid;

                    if (kurs.mid > 0)
                        kursWaluty.KursSredni = kurs.mid;

                    if (kurs.ask > 0)
                        kursWaluty.KursSprzedazy = kurs.ask;
                }


            }
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
