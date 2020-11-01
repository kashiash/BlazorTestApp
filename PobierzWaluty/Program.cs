using Common.Module.Utils;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using KrajeWaluty.Module.BusinessObjects;
using KrajeWaluty.Module.Utils;
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

            KursyNBPHelper.PobierzKursyNBP("A");
            KursyNBPHelper.PobierzKursyNBP( "C");

            Console.WriteLine("Zakonczono import wciśnij enter aby zakopńczyc program");
            Console.ReadLine();
        }






    }

}
