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

        public Liquid(string name, string id, int amount, string information, WarePublisher warePublisher) : base(name, id, amount, information, warePublisher)
        {
        }


        /// <summary>
        /// Constructor that also sets the boiling, maximum and minimum temperature. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="minTemp"></param>
        /// <param name="boilingPoint"></param>
        /// <param name="warePublisher"></param>
        public Liquid(string name, string id, int amount, double minTemp, float? boilingPoint, WarePublisher warePublisher) : base(name, id, amount, warePublisher)
        {
            this.minTemp = minTemp;
            this.boilingPoint = boilingPoint;
        }

        public Liquid(string name, string id, int amount, string information, double minTemp, float? boilingPoint, WarePublisher warePublisher) : base(name, id, amount, information, warePublisher)
        {
            this.minTemp = minTemp;
            this.boilingPoint = boilingPoint;
        }


        [WareSeacheable("Minimum Temperature")]
        /// <summary>
        /// Gets the minimum temperature the materiale is liquid at.
        /// </summary>
        public double MinimumTemperature { get => minTemp; }

        [WareSeacheable("Boiling Point")]
        /// <summary>
        /// ... null indicates the boiling point has not been set.
        /// </summary>
        public float? GetBoilingPoint { get => boilingPoint; }

        [WareSeacheable("Volatile")]
        /// <summary>
        /// 
        /// </summary>
        public bool IsVolatile { get => @volatile; }


    }
}
