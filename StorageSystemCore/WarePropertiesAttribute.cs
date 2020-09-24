using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    public class WareSeacheableAttribute : Attribute
    {
        /// <summary>
        /// The name of the attribute.
        /// </summary>
        private string name;
        /// <summary>
        /// The name of the attribute as it is in the sql database.
        /// </summary>
        private string sqlName;

        /// <summary>
        /// The default constructor for WareSeacheableAttribute class 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sqlName"></param>
        public WareSeacheableAttribute(string name, string sqlName)
        {
            this.name = name;
            this.sqlName = sqlName;
        }

        /// <summary>
        /// Gets and sets the name of the attribute.
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Gets and sets the name of the attribute as it is in the SQL database.
        /// </summary>
        public string SQLName { get => sqlName; set => sqlName = value; }

    }
}
