using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    public class WarePropertiesAttributes : Attribute
    {
        private string property;

        public WarePropertiesAttributes(string property)
        {
            this.property = property;
        }

        public string Property { get => property; set => property = value; }


    }
}
