﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    static class WareInformation
    {
        /// <summary>
        /// Llist contains all wares
        /// </summary>
        private static List<Ware> wares = new List<Ware>(); //have a class for storaging/manipulating the ware list

        /// <summary>
        /// Gets a deep-copy of all wares.
        /// </summary>
        public static List<Ware> Ware { get => Support.DeepCopy(wares); set => wares = value; } 

        /// <summary>
        /// Gets ...
        /// </summary>
        /// <returns></returns>
        public static List<string[]> GetWareInformation()
        {
            List<string[]> wareInformation = new List<string[]>();
            foreach (Ware ware in wares)
            {
                string[] information = new string[4]; //use reflection to find these values, e.g. each Ware function/property contains something like 
                //[Data(isData:bool,dataType:string] [Data(true,"Name")] or [Data(true,"ID")] and then here they are put in array after the order of the dataTypes. 
                //Also the storage class should contain functions/properties that only return specific values, e.g. IDs or Names
                information[0] = ware.GetName;
                information[1] = ware.GetID;
                information[2] = ware.GetAmount.ToString();
                information[3] = FindTypeAttribute(ware);//ware.GetType().ToString().Split('.')[1]; //consider using reflection for the type, since the namespace is returned
                wareInformation.Add(information);
            }
            return wareInformation;
        }

        public static void AddWareTEst(string name, string id, string type, int amount) //move later to its final class 
        {
            Type test = Type.GetType("LagerSystem."+type);
            wares.Add((Ware)Activator.CreateInstance(test, new object[]{name,id,amount, Publisher.PubWare }));
        }

        public static void AddWare() //when storage class has been added move this function to it
        {
            wares.Add(new Liquid("Test", "ID-55t", 25, Publisher.PubWare));
            wares.Add(new Electronic("Toaster", "ID-123Q", 2, Publisher.PubWare));
            wares.Add(new Liquid("Superproduct", "ID-55t2", 1, Publisher.PubWare));
        }

        /// <summary>
        /// Removes the ware with the <paramref name="ID"/>. Returns true if the ware was found and removed, else false.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool RemoveWare(string ID) //when storage class has been added move this function to it
        {
            for (int i = wares.Count - 1; i >= 0; i--)
                if (wares[i].GetID == ID)
                {
                    wares[i].RemoveSubscriptions(Publisher.PubWare);
                    wares.RemoveAt(i);
                    return true;
                }
            return false;
        }

        /// <summary>
        /// Finds and returns a string with all attributes of <c>WareTypeAttribute</c> belonging to <paramref name="ware"/>.
        /// </summary>
        /// <param name="ware"></param>
        /// <returns></returns>
        private static string FindTypeAttribute(Ware ware ) //could it be modified to find different Attributes?
        {

            string typeString = "";
            Attribute[] attributes = Attribute.GetCustomAttributes(ware.GetType());
            foreach (Attribute attr in attributes)
                if (attr is WareTypeAttribute info)
                    typeString += info.Type;
            return typeString;
        }

    }
}
