﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    static class WareInformation
    {
        private static List<Ware> wares = new List<Ware>();

        public static List<Ware> Ware { get => Support.DeepCopy(wares); set => wares = value; } //the get should return a deep copy

        public static List<string[]> GetWareInformation()
        {
            List<string[]> wareInformation = new List<string[]>();
            string[] information = new string[4]; //Name, ID, Amount, Type (class type that is, e.g. Liquid)
            foreach (Ware ware in wares)
            {
                information[0] = ware.GetName;
                information[1] = ware.GetID;
                information[2] = ware.GetAmount.ToString();
                information[3] = ware.GetType().ToString(); //consider using reflection for the type
                wareInformation.Add(information);
            }
            return wareInformation;
        }

        public static void AddWare()
        {
            wares.Add(new Liquids("Test", "ID-55t", 25, Publisher.PubWare));
        }

    }
}
