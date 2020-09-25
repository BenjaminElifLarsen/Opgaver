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

        public static void ObjectToSQL(object ware) //calls the SQLControl in the end
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
            SQLCode.SQLControl.AddWare("Inventory", null, null); //have a function that goes through the keys and puts them into an array and a function for values that puts them into an array
        }

    }
}
