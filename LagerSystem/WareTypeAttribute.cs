using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    public class WareTypeAttribute : Attribute
    {
        private string type;
        public WareTypeAttribute(string type)
        {
            this.type = type;
        }
        public string Type { get => type; }

    }
}
