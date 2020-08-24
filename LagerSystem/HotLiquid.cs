using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public HotLiquid(string name, string id, int amount, double minTemp, double maxTemp, WarePublisher warePublisher) : base(name, id, amount, warePublisher)
        {
            this.minTemp = minTemp;
            this.maxTemp = maxTemp;
        }

        
    }
}
