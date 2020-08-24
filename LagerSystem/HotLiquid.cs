using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    [WareType("HotLiquid")]
    class HotLiquid : Liquid
    {
        public HotLiquid(string name, string id, int amount, WarePublisher warePublisher) : base(name, id, amount, warePublisher)
        {
        }

        public double MinimumTemperatur { get; }
    }
}
