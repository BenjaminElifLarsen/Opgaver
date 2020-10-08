﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystemCore
{
    /// <summary>
    /// Continas methods and properties related to ware information. 
    /// </summary>
    public static class WareInformation
    {
        /// <summary>
        /// The names of the basic variables that all ware and derived constructors contains.  
        /// </summary>
        private static List<string> baseCtorVariables = new List<string>() { "name", "id", "amount" };

        /// <summary>
        /// The sql names of all basic variables that all wares shall contain.
        /// </summary>
        private static List<string> baseSQLNames = new List<string>() { "name", "id", "amount" };

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
        /// Gets a list with the sql names that all wares shall have set. 
        /// </summary>
        public static List<string> BasicSQLNames { get => baseSQLNames; }

        /// <summary>
        /// Gets the basic information that all wares needs to contain
        /// </summary>
        /// <returns></returns>
        public static List<string[]> GetWareInformation()
        {
            List<string[]> wareInformation = new List<string[]>();
            foreach (Ware ware in wares)
            {
                string[] information = new string[4];
                information[0] = ware.GetName;
                information[1] = ware.GetID;
                information[2] = ware.GetAmount.ToString();
                information[3] = FindTypeAttribute(ware);
                wareInformation.Add(information);
            }
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
                foreach (PropertyInfo propertyInfo in propertyInfoArray)
                {
                    foreach(Attribute attribute in propertyInfo.GetCustomAttributes())
                    {
                        if(attribute.GetType() == typeof(WareSeacheableAttribute)) 
                        { 
                            WareSeacheableAttribute seacheableAttribute = attribute as WareSeacheableAttribute;
                            if (attributesToSearchFor.Contains(seacheableAttribute.Name)) {
                                object value = propertyInfo.GetValue(ware); //needs to deal with arrays, lists and such
                                if(value == null && propertyInfo.PropertyType == typeof(string))
                                    value = "null";
                                wareInformation[wareInformation.Count - 1].Add(seacheableAttribute.Name, value); 
                            }
                        }
                    }
                }
                if (attributesToSearchFor.Contains("Type")) //Type got its own if-statment, since its attribute is connected to the class rather than properties. 
                {
                    WareTypeAttribute attribute = ware.GetType().GetCustomAttribute(typeof(WareTypeAttribute)) as WareTypeAttribute;
                    wareInformation[wareInformation.Count - 1].Add("Type", attribute.Type);
                }

            }
            return wareInformation;
        }

        /// <summary>
        /// Adds a new ware of <paramref name="type"/> with the basic values of <paramref name="name"/>, <paramref name="id"/> and <paramref name="amount"/>. 
        /// More arguments can be given in <paramref name="extra"/> in an order that fits a constructor of <paramref name="type"/>.
        /// </summary>
        /// <param name="name">The name of the ware.</param>
        /// <param name="id">The id of the ware.</param>
        /// <param name="type">The type of the ware.</param>
        /// <param name="amount">The amount of the ware.</param>
        /// <param name="extra">Extra information for the constructor of <paramref name="type"/>.</param>
        public static void AddWare(string name, string id, string type, int amount, object[] extra) //move later to its final class, the WareCreator (after all, that is the calls that creates wares)
        {
            if(type.Split(' ').Length != 1)
            {
                string[] split = type.Split(' ');
                type = "";
                foreach (string typing in split)
                    type += typing;
            }
            Type test = Type.GetType("StorageSystemCore." + type);
            int lengthToAdd = extra != null ? extra.Length : 0;
            object[] dataObject = new object[4 + lengthToAdd];
            dataObject[0] = name;
            dataObject[1] = id;
            dataObject[2] = amount;
            for (int i = 3; i < dataObject.Length - 1; i++)
                dataObject[i] = extra[i - 3];
            dataObject[dataObject.Length-1] = Publisher.PubWare;
            wares.Add((Ware)Activator.CreateInstance(test, dataObject ));
        }

        /// <summary>
        /// Adds a new ware of <paramref name="type"/> with the basic values of <paramref name="name"/>, <paramref name="id"/> and <paramref name="amount"/>.
        /// Creates the ware in the database. 
        /// </summary>
        /// <param name="name">The name of the ware.</param>
        /// <param name="id">The ID of the ware.</param>
        /// <param name="type">The type of the ware.</param>
        /// <param name="amount">The amount of the ware.</param>
        public static void AddWare(string name, string id, string type, int amount) //move later to its final class, the WareCreator (after all, that is the calls that creates wares)
        {
            SQLCode.StoredProcedures.InsertWareSP($"'{id}'", $"'{name}'", amount, $"'{type}'");
        }

        /// <summary>
        /// Adds a new ware of <paramref name="type"/> with the basic values of <paramref name="name"/>, <paramref name="id"/> and <paramref name="amount"/>.
        /// Information stored in <paramref name="columnsAndValues"/> will be used to add extra information about the ware. 
        /// </summary>
        /// <param name="name">The name of the ware.</param>
        /// <param name="id">The ID of the ware.</param>
        /// <param name="type">The type of the ware.</param>
        /// <param name="amount">The amount of the ware. </param>
        /// <param name="columnsAndValues">Extra information to add to the ware. The keys are sql columns.</param>
        public static void AddWare(string name, string id, string type, int amount, Dictionary<string,object> columnsAndValues)
        {
            object information;
            columnsAndValues.TryGetValue("information",out information);
            string informationString = $"'{information}'" == "" ? null : $"'{information}'";

            object dangerCategory;
            columnsAndValues.TryGetValue("dangerCategory", out dangerCategory);
            string dangerCategoryString = $"{dangerCategory}" == "" ? null : $"{dangerCategory}";

            object flashPoint;
            columnsAndValues.TryGetValue("flashPoint", out flashPoint);
            string flashPointString = $"{flashPoint}" == "" ? null : $"{flashPoint}";

            object minTemp;
            columnsAndValues.TryGetValue("minTemp", out minTemp);
            string minTempString = $"{minTemp}" == "" ? null : $"{minTemp}";

            object boilingPoint;
            columnsAndValues.TryGetValue("boilingPoint", out boilingPoint);
            string boilingPointString = $"{boilingPoint}" == "" ? null : $"{boilingPoint}";

            object @volatile;
            columnsAndValues.TryGetValue("@volatile", out @volatile);
            string @volatileString = $"{@volatile}" == "" ? null : $"{@volatile}";

            SQLCode.StoredProcedures.InsertWareSP($"'{id}'", $"'{name}'", amount, $"'{type}'", informationString, dangerCategoryString, flashPointString, minTempString, boilingPointString, @volatileString);
        }

        /// <summary>
        /// Adds default wares if no database has been selected. 
        /// </summary>
        public static void AddWareDefault() //when storage class has been added move this function to it
        {
            wares.Add(new Liquid("Water", "ID-55t", 25, Publisher.PubWare));
            wares.Add(new Electronic("Toaster", "ID-123q", 2, Publisher.PubWare));
            wares.Add(new Liquid("Milk", "ID-55t2", 1, Publisher.PubWare));
            wares.Add(new CombustibleLiquid("FOOF", "ID-5q1", 10, -163, 1, -57, null, Publisher.PubWare));
            wares.Add(new Electronic("TV", "ID-tv4", 512, "This is an ordinary television. Please buy it.", Publisher.PubWare));
            wares.Add(new CombustibleLiquid("CiF3", "ld-5wQ", 1, "One of the world most deadly chemicals.", Publisher.PubWare));
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
        private static string FindTypeAttribute(Ware ware ) //could/should it be modified to find different Attributes?
        {
            string typeString = "";
            Attribute[] attributes = Attribute.GetCustomAttributes(ware.GetType());
            foreach (Attribute attr in attributes)
                if (attr is WareTypeAttribute info)
                    typeString += info.Type;
            return typeString;
        }

        /// <summary>
        /// Finds all and returns all names and sqlNames of WareSeacheableAttribute.
        /// </summary>
        /// <param name="type">The type to find all searchable attributse of.</param>
        /// <returns></returns>
        public static List<string[]> FindSearchableAttributes(Type type)
        {
            List<string[]> properties = new List<string[]>();
            PropertyInfo[] propertyInfos = type.GetProperties();
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

        /// <summary>
        /// Gets all extra constructors of <paramref name="type"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="extraParameters"></param>
        /// <returns></returns>
        public static List<Dictionary<string,Type>> GetConstructorParameterNamesAndTypes(Type type, string[] extraParameters)
        {
            List<Dictionary<string,Type>> constructors = new List<Dictionary<string, Type>>();
            ConstructorInfo[] constructorInfos = type.GetConstructors();
            foreach (ConstructorInfo constructorInfo in constructorInfos)
            {
                constructors.Add(new Dictionary<string, Type>());

                foreach (ParameterInfo parameterInfo in constructorInfo.GetParameters()) 
                {
                    if (parameterInfo.ParameterType != typeof(WarePublisher))
                        if(!baseCtorVariables.Contains(parameterInfo.Name))
                            constructors[constructors.Count - 1].Add(parameterInfo.Name,parameterInfo.ParameterType);
                }
            }
            constructors.RemoveAt(0);
            return constructors; 
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
        /// <param name="sql">True for finding the sql names, false for finding the non-sql names.</param>
        /// <returns></returns>
        public static List<string> FindAllSearchableAttributesNames(bool sql) //rename
        {
            List<string> listOfTypes = FindWareTypes(); //have a function that calls this one and returns name, one for sqlnames and one for both
            List<string> searchable = new List<string>();
            byte column = sql ? (byte)1 : (byte)0;
            foreach (string type in listOfTypes)
            {
                string type_ = Support.RemoveSpace(type);
                List<string[]> attributes = FindSearchableAttributes(Type.GetType("StorageSystemCore." + type_));
                foreach (string[] attrArray in attributes)
                    if (!searchable.Contains(attrArray[column]))
                        searchable.Add(attrArray[column]);
            }
            return searchable;
        }

    }
}
