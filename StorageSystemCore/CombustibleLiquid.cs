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

        public CombustibleLiquid(string name, string id, string information, int amount, WarePublisher warePublisher) : base(name, id, information, amount, warePublisher)
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
        /// <param name="boilingPoint"></param>
        /// <param name="warePublisher"></param>
        public CombustibleLiquid(string name, string id, int amount, double minTemp, float? boilingPoint, WarePublisher warePublisher) : base(name, id, amount, minTemp, boilingPoint, warePublisher)
        {
            category = null;
            flashPoint = null;
        }

        public CombustibleLiquid(string name, string id, string information, int amount, double minTemp, float? boilingPoint, WarePublisher warePublisher) : base(name, id, information, amount, minTemp, boilingPoint, warePublisher)
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
        /// <param name="category"></param>
        /// <param name="boilingPoint"></param>
        /// <param name="flashPoint"></param>
        /// <param name="warePublisher"></param>
        public CombustibleLiquid(string name, string id, int amount, double minTemp, byte category, float boilingPoint, float?[] flashPoint, WarePublisher warePublisher) : base(name, id, amount, minTemp, boilingPoint, warePublisher)
        {
            this.category = category;
            this.flashPoint = flashPoint;
        }

        public CombustibleLiquid(string name, string id, string information, int amount, double minTemp, byte category, float boilingPoint, float?[] flashPoint, WarePublisher warePublisher) : base(name, id, information, amount, minTemp, boilingPoint, warePublisher)
        {
            this.category = category;
            this.flashPoint = flashPoint;
        }

        [WareSeacheable("Category")]
        /// <summary>
        /// Gets the category of the liquid. Null indicates the category has not been set.
        /// </summary>
        public float? GetCategory { get => category; }

        [WareSeacheable("Flash Point")]
        /// <summary>
        /// Gets the flash point of the liquid. Null indicates the flashpoint has not been set. 
        /// </summary>
        public float?[] GetFlashPoint { get => flashPoint; }

    }
}
