using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    public class WareAttribute : Attribute
    {
        private string type;
        public WareAttribute(string type)
        {
            this.type = type;
        }
        public string Type { get => type; }

    }
}
