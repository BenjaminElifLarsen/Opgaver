using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace StorageSystemCore
{
    /// <summary>
    /// "Converts" a class object to information in the SQL-database and information in the SQL database to a class object 
    /// </summary>
    public static class ObjectSQLConversion
    {
        /// <summary>
        /// Converts an object to a dictionary<string,object> where all strings has '' added around them.
        /// All sql properties of string types that returns an empty string has their value replaced with "null".
        /// </summary>
        /// <param name="ware"></param>
        /// <returns></returns>
        public static Dictionary<string,object> ObjectToSQL(object ware) //needs to add the type too
        {
            Dictionary<string, object> info = new Dictionary<string, object>();
            PropertyInfo[] propertyInfoArray = ware.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfoArray)
            {
                foreach (Attribute attribute in propertyInfo.GetCustomAttributes())
                {
                    if (attribute.GetType() == typeof(WareSeacheableAttribute))
                    { 
                        WareSeacheableAttribute seacheableAttribute = attribute as WareSeacheableAttribute;
                        object value = propertyInfo.GetValue(ware); 
                        if (value == null && propertyInfo.PropertyType == typeof(string))
                            value = "null";
                        if (propertyInfo.PropertyType == typeof(string))
                            value = $"'{value}'";
                        info.Add(seacheableAttribute.Name, value); 
                    }
                }
            }
            return info;    
        }

        public static object SQLToObject(string query) //not designed to select specific values of a sql entry, but rather creating an object from all columns
        { //so the first columns of the query should always be in the order of the basic constructor and the rest of the columns should be set 
            //via properties if the type contains those specific columns 
            //maybe return array of all objects in the database
            throw new NotImplementedException();
        }

    }
}
