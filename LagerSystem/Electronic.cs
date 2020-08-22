using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    [WareType("Electronic")]
    sealed class Electronic : Ware
    {
        public Electronic(string name, string id, int amount, WarePublisher warePublisher) : base(name, id, amount, warePublisher)
        {
        }
    }
}
