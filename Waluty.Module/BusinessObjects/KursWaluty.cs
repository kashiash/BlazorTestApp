using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrajeWaluty.Module.BusinessObjects
{
    public class KursWaluty : XPObject
    {
        public KursWaluty(Session session) : base(session)
        { }


        DateTime dataKursu;
        decimal kursSredni;

        decimal kursSprzedazy;
        decimal kursSkupu;
        Waluta waluta;

        [Association("Waluta-KursyWalut")]
        public Waluta Waluta
        {
            get => waluta;
            set => SetPropertyValue(nameof(Waluta), ref waluta, value);
        }

        
        public DateTime DataKursu
        {
            get => dataKursu;
            set => SetPropertyValue(nameof(DataKursu), ref dataKursu, value);
        }
        public decimal KursSkupu
        {
            get => kursSkupu;
            set => SetPropertyValue(nameof(KursSkupu), ref kursSkupu, value);
        }


        public decimal KursSredni
        {
            get => kursSredni;
            set => SetPropertyValue(nameof(KursSredni), ref kursSredni, value);
        }

        public decimal KursSprzedazy
        {
            get => kursSprzedazy;
            set => SetPropertyValue(nameof(KursSprzedazy), ref kursSprzedazy, value);
        }
    }
}
