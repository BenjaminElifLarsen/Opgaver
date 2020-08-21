using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    static class WareInformation
    {
        private static List<Ware> wares = new List<Ware>(); //have a class for storaging the ware list

        public static List<Ware> Ware { get => Support.DeepCopy(wares); set => wares = value; } 

        public static List<string[]> GetWareInformation()
        {
            List<string[]> wareInformation = new List<string[]>();
            foreach (Ware ware in wares)
            {
                string[] information = new string[4]; 
                information[0] = ware.GetName;
                information[1] = ware.GetID;
                information[2] = ware.GetAmount.ToString();
                information[3] = FindTypeAttribute(ware);//ware.GetType().ToString().Split('.')[1]; //consider using reflection for the type, since the namespace is returned
                wareInformation.Add(information);
            }
            return wareInformation;
        }

        public static void AddWare() //when storage class has been added move this function to it
        {
            wares.Add(new Liquids("Test", "ID-55t", 25, Publisher.PubWare));
            wares.Add(new Liquids("Superproduct", "ID-55t2", 1, Publisher.PubWare));
        }

        public static bool RemoveWare(string ID) //when storage class has been added move this function to it
        {
            for (int i = wares.Count - 1; i >= 0; i--)
                if (wares[i].GetID == ID)
                {
                    wares.RemoveAt(i);
                }
            return false;
        }

        private static string FindTypeAttribute(Ware ware )
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
