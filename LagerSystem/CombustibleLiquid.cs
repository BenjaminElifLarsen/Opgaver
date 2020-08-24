using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    [WareType("Combustible Liquid")]
    class CombustibleLiquid : Liquid
    {
        /// <summary>
        /// The flammable category of the liquid. Goes from 1 (dangerous) to 4 (safest).
        /// </summary>
        protected byte? category;
        /// <summary>
        /// The flashpount of the liquid.
        /// </summary>
        protected float?[] flashPoint;

        /// <summary>
        /// Most basic constructor, derived from Ware. Category, boilingPoint and flashPoint need to be given after.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="warePublisher"></param>
        public CombustibleLiquid(string name, string id, int amount, WarePublisher warePublisher) : base(name, id, amount, warePublisher)
        {
            category = null;
            boilingPoint = null;
            flashPoint = null;
        }

        /// <summary>
        /// Basic liquid constructor, derived from Liquid. Category and flashPoints need to be given after. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="minTemp"></param>
        /// <param name="maxTemp"></param>
        /// <param name="boilingPoint"></param>
        /// <param name="warePublisher"></param>
        public CombustibleLiquid(string name, string id, int amount, double minTemp, double maxTemp, float? boilingPoint, WarePublisher warePublisher) : base(name, id, amount, maxTemp, minTemp, boilingPoint, warePublisher)
        {
            category = null;
            flashPoint = null;
        }

        /// <summary>
        /// Constructor for a combustible liquid. No properties need to be set after the constructor call.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="minTemp"></param>
        /// <param name="maxTemp"></param>
        /// <param name="category"></param>
        /// <param name="boilingPoint"></param>
        /// <param name="flashPoint"></param>
        /// <param name="warePublisher"></param>
        public CombustibleLiquid(string name, string id, int amount, double minTemp, double maxTemp, byte category, float boilingPoint, float?[] flashPoint, WarePublisher warePublisher) : base(name, id, amount, maxTemp, minTemp, boilingPoint, warePublisher)
        {
            this.category = category;
            this.flashPoint = flashPoint;
        }

        /// <summary>
        /// ... null indicates the category has not been set.
        /// </summary>
        public float? GetCategory { get => category; }

        /// <summary>
        /// ... null indicates the flashpoint has not been set. 
        /// </summary>
        public float?[] GetFlashPoint { get => flashPoint; }

    }
}
