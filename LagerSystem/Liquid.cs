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
        protected float? boilingPoint;
        protected bool @volatile;
        /// <summary>
        /// The basic ware consturctor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="warePublisher"></param>
        public Liquid(string name, string id, int amount, WarePublisher warePublisher) : base(name, id, amount, warePublisher) 
        {
        }

        /// <summary>
        /// Constructor that also sets the boiling, maximum and minimum temperature. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="maxTemp"></param> //max temp would also be the boilingPoint
        /// <param name="minTemp"></param>
        /// <param name="warePublisher"></param>
        public Liquid(string name, string id, int amount, double maxTemp, double minTemp, float? boilingPoint, WarePublisher warePublisher) : base(name, id, amount, warePublisher)
        {
            this.minTemp = minTemp;
            this.maxTemp = maxTemp;
            this.boilingPoint = boilingPoint;
        }

        /// <summary>
        /// Gets the minimum temperature the materiale is liquid at.
        /// </summary>
        public double MinimumTemperatur { get => minTemp; }

        /// <summary>
        /// Gets the maximum temperature the materiale is liquid at. 
        /// </summary>
        public double MaximumTemperatur { get => maxTemp; }

        /// <summary>
        /// ... null indicates the boiling point has not been set.
        /// </summary>
        public float? GetBoilingPoint { get => boilingPoint; }

        public bool IsVolatile { get => @volatile; }


    }
}
