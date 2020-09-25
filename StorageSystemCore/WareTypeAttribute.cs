using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystemCore
{
    /// <summary>
    /// ... The value needs to be same as class name
    /// </summary>
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
