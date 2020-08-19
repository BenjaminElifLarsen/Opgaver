using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    [Ware("Liquid")]
    sealed class Liquids : Ware
    {
        public Liquids(string name, string id, uint amount, WarePublisher warePublisher) : base(name, id, amount, warePublisher) { }


    }
}
