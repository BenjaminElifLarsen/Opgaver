using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    [WareType("Liquid")]
    sealed class Liquid : Ware
    {
        public Liquid(string name, string id, int amount, WarePublisher warePublisher) : base(name, id, amount, warePublisher) 
        {
        }


    }
}
