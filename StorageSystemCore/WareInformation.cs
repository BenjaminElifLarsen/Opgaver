using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystemCore
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
        /// 
        /// </summary>
        /// <param name="attributesToSearchFor"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> GetWareInformation(List<string> attributesToSearchFor)
        {
            if (attributesToSearchFor == null)
                throw new NullReferenceException();
            List<Dictionary<string, object>> wareInformation = new List<Dictionary<string, object>>(); 
            foreach(Ware ware in wares)
            {
                wareInformation.Add(new Dictionary<string, object>());
                List<string> information = new List<string>();
                PropertyInfo[] propertyInfoArray = ware.GetType().GetProperties();
                foreach(PropertyInfo propertyInfo in propertyInfoArray)
                {
                    foreach(Attribute attribute in propertyInfo.GetCustomAttributes())
                    {
                        if(attribute.GetType() == typeof(WareSeacheableAttribute))
                        { //FindSearchableAttributes has the problem solved using a list, but figure out a why to solve this problem without
                            WareSeacheableAttribute seacheableAttribute = attribute as WareSeacheableAttribute;
                            if (attributesToSearchFor.Contains(seacheableAttribute.Name)) { //returns "\"value\""  
                                object value = propertyInfo.GetValue(ware); /*!= null ? propertyInfo.GetValue(ware) : null;*/ //needs to deal with arrays, lists and such
                                if(value == null && propertyInfo.PropertyType == typeof(string))
                                    value = "null";
                                wareInformation[wareInformation.Count - 1].Add(seacheableAttribute.Name, value); //have an attribute for collections, i.e. true or false
                            }
                        }
                    }
                }
            }
            return wareInformation;
        }

        /// <summary>
        /// Adds a new ware of <paramref name="type"/> with the values of <paramref name="name"/>, <paramref name="id"/> and <paramref name="amount"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        public static void AddWare(string name, string id, string type, int amount, object[] extra) //move later to its final class 
        {
            if(type.Split(' ').Length != 1)
            {
                string[] split = type.Split(' ');
                type = "";
                foreach (string typing in split)
                    type += typing;
            }
            Type test = Type.GetType("LagerSystem."+type);
            object[] dataObject = new object[4+extra.Length];
            dataObject[0] = name;
            dataObject[1] = id;
            dataObject[2] = amount;
            for (int i = 3; i < dataObject.Length - 1; i++)
                dataObject[i] = extra[i - 3];
            dataObject[dataObject.Length-1] = Publisher.PubWare;
            wares.Add((Ware)Activator.CreateInstance(test, dataObject ));
        }

        public static void AddWareDefault() //when storage class has been added move this function to it
        {
            wares.Add(new Liquid("Water", "ID-55t", 25, Publisher.PubWare));
            wares.Add(new Electronic("Toaster", "ID-123q", 2, Publisher.PubWare));
            wares.Add(new Liquid("Milk", "ID-55t2", 1, Publisher.PubWare));
            wares.Add(new CombustibleLiquid("FOOF", "ID-5q1", 10, -163, 1, -57, null, Publisher.PubWare));
            wares.Add(new Electronic("TV", "ID-tv4", 512, "This is an ordinary television. Please buy it.", Publisher.PubWare));
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
        public static List<string[]> FindSearchableAttributes(Type type) //consider altering to return both name and sql name 
        {
            List<string[]> properties = new List<string[]>();
            //string typeString = ""; //maybe return a dictionary for the data type of the property to know what the user can enter.
            PropertyInfo[] propertyInfos = type.GetProperties();
            //Attribute[] attributes = type.GetMethods();
            foreach (PropertyInfo propertyInfo in propertyInfos)
                foreach (Attribute attre in propertyInfo.GetCustomAttributes())
                    if (attre is WareSeacheableAttribute info)
                        properties.Add(new string[] { info.Name, info.SQLName });
            
            return properties;
        }

        /// <summary>
        /// Finds and returns the name of all parameters belogning to each constructor of type <paramref name="type"/>
        /// </summary>
        /// <param name="type">The type to find all constructors of.</param>
        public static List<List<string>> FindConstructorsParameterNames(Type type)
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

        public static List<Dictionary<string,Type>> GetConstructorParameterNamesAndTypes(Type type, string[] extraParameters)
        {
            List<Dictionary<string,Type>> constructors = new List<Dictionary<string, Type>>();
            ConstructorInfo[] constructorInfos = type.GetConstructors();
            foreach (ConstructorInfo constructorInfo in constructorInfos)
            {
                constructors.Add(new Dictionary<string, Type>());

                foreach (ParameterInfo parameterInfo in constructorInfo.GetParameters()) //could make it such that it contains an if-statment that ensures the amount of parameters are the same in extraarametesr + the base amount
                {
                    if (parameterInfo.ParameterType != typeof(WarePublisher))
                        if(!baseCtorVariables.Contains(parameterInfo.Name))
                            constructors[constructors.Count - 1].Add(parameterInfo.Name,parameterInfo.ParameterType);
                }
            }
            constructors.RemoveAt(0);
            return constructors; //create a default version of the value (using the new support function) instead of Type, this means you should be able to use the new WareCreator methods better. 
            throw new NotImplementedException();
        }

        public static List<string> FindWareTypes() 
        {
            List<string> typeList = new List<string>();
            Type[] types = Assembly.GetExecutingAssembly().GetTypes(); //finds all types in the executing assembly
            foreach (Type type in types)
            {
                List<Attribute> attrs = type.GetCustomAttributes().ToList(); //converts their custom attributes to a list
                if (!type.IsAbstract) //ensures the base class is not a "valid" type since it is abstract
                    foreach (Attribute attr in attrs)
                        if (attr is WareTypeAttribute info) //is the attribute the correct one
                        {
                            typeList.Add(info.Type); //add to list
                            break;
                        }
            }
            return typeList;
        }

        /// <summary>
        /// Finds and returns the name of all searchable attributes over all classes that inherences from <c>Ware</c>.
        /// </summary>
        /// <returns></returns>
        public static List<string> FindAllSearchableAttributesNames() //rename
        {
            List<string> listOfTypes = FindWareTypes(); //have a function that calls this one and returns name, one for sqlnames and one for both
            List<string> searchable = new List<string>();

            foreach (string type in listOfTypes)
            {
                string type_ = type;
                if (type_.Split(' ').Length != 1) //this if-statment exist in two locations, create a support function for this. 
                {
                    string[] split = type.Split(' ');
                    type_ = "";
                    foreach (string typing in split)
                        type_ += typing;
                }
                List<string[]> attributes = FindSearchableAttributes(Type.GetType("LagerSystem." + type_));
                foreach (string[] attrArray in attributes)
                    if (!searchable.Contains(attrArray[0]))
                        searchable.Add(attrArray[0]);
            }
            return searchable;
        }

    }
}
