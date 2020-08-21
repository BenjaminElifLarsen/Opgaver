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
                information[3] = FindTypeAttribute(ware);//ware.GetType().ToString().Split('.')[1]; //consider using reflection for the type, since the namespace is returned
                wareInformation.Add(information);
            }
            return wareInformation;
        }

        public static void AddWare()
        {
            wares.Add(new Liquids("Test", "ID-55t", 25, Publisher.PubWare));
        }

        private static string FindTypeAttribute(Ware ware )
        {

            string typeString = "";
            Attribute[] attributes = Attribute.GetCustomAttributes(ware.GetType());
            foreach (Attribute attr in attributes)
                if (attr is WareTypeAttribute)
                {
                    WareTypeAttribute info = (WareTypeAttribute)attr;
                    typeString += info.Type;
                }
            return typeString;
        }

    }
}
