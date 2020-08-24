using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    [WareType("Liquid")]
    class Liquid : Ware
    {
        protected double minTemp;
        protected double maxTemp;
        public Liquid(string name, string id, int amount, WarePublisher warePublisher) : base(name, id, amount, warePublisher) 
        {
        }


        public double MinimumTemperatur { get => minTemp; }

        public double MaximumTemperatur { get => maxTemp; }
    }
}
