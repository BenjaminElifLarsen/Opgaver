using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    public class WareSeacheableAttribute : Attribute
    {
        private string property;

        public WareSeacheableAttribute(string property)
        {
            this.property = property;
        }

        public string Property { get => property; set => property = value; }


    }
}
