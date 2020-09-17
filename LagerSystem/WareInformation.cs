using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    static class WareInformation
    {
        /// <summary>
        /// The names of the basic variables that all ware and derived constructors contains.  
        /// </summary>
        private static List<string> baseCtorVariables = new List<string>() { "name", "amount", "id" };

        /// <summary>
        /// Llist contains all wares
        /// </summary>
        private static List<Ware> wares = new List<Ware>(); //have a class for storaging/manipulating the ware list

        /// <summary>
        /// Gets a deep-copy of all wares.
        /// </summary>
        public static List<Ware> Ware { get => Support.DeepCopy(wares); set => wares = value; } 

        /// <summary>
        /// Gets a list with the parameter names that all constructors of Ware and derived are using. 
        /// </summary>
        public static List<string> BasicConstructorVariableNames { get => baseCtorVariables; }


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
            //FindSearchableAttributes(typeof(CombustibleLiquid));
            return wareInformation;
        }

        /// <summary>
        /// Adds a new ware of <paramref name="type"/> with the values of <paramref name="name"/>, <paramref name="id"/> and <paramref name="amount"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        public static void AddWare(string name, string id, string type, int amount) //move later to its final class 
        {
            if(type.Split(' ').Length != 1)
            {
                string[] split = type.Split(' ');
                type = "";
                foreach (string typing in split)
                    type += typing;
            }
            Type test = Type.GetType("LagerSystem."+type);
            wares.Add((Ware)Activator.CreateInstance(test, new object[]{name,id,amount, Publisher.PubWare }));
        }

        public static void AddWareDefault() //when storage class has been added move this function to it
        {
            wares.Add(new Liquid("Test", "ID-55t", 25, Publisher.PubWare));
            wares.Add(new Electronic("Toaster", "ID-123q", 2, Publisher.PubWare));
            wares.Add(new Liquid("Superproduct", "ID-55t2", 1, Publisher.PubWare));
            wares.Add(new CombustibleLiquid("FOOF", "ID-5q1", 10, -163, 1, -57, null, Publisher.PubWare));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">The type to find all searchable attributse of.</param>
        /// <returns></returns>
        private static List<string> FindSearchableAttributes(Type type)
        {
            List<string> properties = new List<string>();
            //string typeString = ""; //maybe return a dictionary for the data type of the property to know what the user can enter.
            PropertyInfo[] propertyInfos = type.GetProperties();
            //Attribute[] attributes = type.GetMethods();
            foreach (PropertyInfo propertyInfo in propertyInfos)
                foreach (Attribute attre in propertyInfo.GetCustomAttributes())
                    if (attre is WareSeacheableAttribute info)
                        properties.Add(info.Property);
                        //typeString += info.Property + " ";
            
            return properties;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">The type to find all constructors of.</param>
        public static List<List<string>> FindConstructors(Type type)
        {
            List<List<string>> constructors = new List<List<string>>();
            ConstructorInfo[] constructorInfos = type.GetConstructors();
            foreach (ConstructorInfo constructorInfo in constructorInfos)
            {
                constructors.Add(new List<string>());

                foreach (ParameterInfo parameterInfo in constructorInfo.GetParameters())
                {
                    if(parameterInfo.ParameterType != typeof(WarePublisher))
                        constructors[constructors.Count - 1].Add(parameterInfo.Name);
                }
            }
            return constructors;
        }

        public static Dictionary<string,dynamic> FindConstructorParameters(Type type, string[] extraParameters)
        {
            return null; //create a default version of the value (using the new support function) instead of Type, this means you should be able to use the new WareCreator methods better. 
            throw new NotImplementedException();
        }

    }
}
