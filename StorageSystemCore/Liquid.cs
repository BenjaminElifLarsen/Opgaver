﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystemCore
{
    [WareType("Liquid")]
    class Liquid : Ware
    {
        protected double? minTemp;
        protected float? boilingPoint;
        protected bool? @volatile;

        /// <summary>
        /// The basic liquid consturctor
        /// </summary>
        /// <param name="name">The name of the ware.</param>
        /// <param name="id">The ID of the ware.</param>
        /// <param name="amount">The unit amount of the ware.</param>
        /// <param name="warePublisher"></param>
        public Liquid(string name, string id, int amount, WarePublisher warePublisher) : base(name, id, amount, warePublisher) 
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">The name of the ware.</param>
        /// <param name="id">The ID of the ware.</param>
        /// <param name="amount">The unit amount of the ware.</param>
        /// <param name="information">Information about the ware.</param>
        /// <param name="warePublisher"></param>
        public Liquid(string name, string id, int amount, string information, WarePublisher warePublisher) : base(name, id, amount, information, warePublisher)
        {
        }


        /// <summary>
        /// Constructor that also sets the boiling, maximum and minimum temperature. 
        /// </summary>
        /// <param name="name">The name of the ware.</param>
        /// <param name="id">The ID of the ware.</param>
        /// <param name="amount">The unit amount of the ware.</param>
        /// <param name="minTemp">The minimum temperature of the ware.</param>
        /// <param name="boilingPoint">The boiling point of the ware.</param>
        /// <param name="warePublisher"></param>
        public Liquid(string name, string id, int amount, double minTemp, float? boilingPoint, WarePublisher warePublisher) : base(name, id, amount, warePublisher)
        {
            this.minTemp = minTemp;
            this.boilingPoint = boilingPoint;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">The name of the ware.</param>
        /// <param name="id">The ID of the ware.</param>
        /// <param name="amount">The unit amount of the ware.</param>
        /// <param name="information">Information about the ware.</param>
        /// <param name="minTemp">The minimum temperature of the ware.</param>
        /// <param name="boilingPoint">The boiling point of the ware.</param>
        /// <param name="warePublisher"></param>
        public Liquid(string name, string id, int amount, string information, double minTemp, float? boilingPoint, WarePublisher warePublisher) : base(name, id, amount, information, warePublisher)
        {
            this.minTemp = minTemp;
            this.boilingPoint = boilingPoint;
        }


        [WareSeacheable("Minimum Temperature", "minTemp")]
        /// <summary>
        /// Gets the minimum temperature the materiale is liquid at.
        /// </summary>
        public double? MinimumTemperature { get => minTemp; set => minTemp = value; }

        [WareSeacheable("Boiling Point","boilingPoint")]
        /// <summary>
        /// ... null indicates the boiling point has not been set.
        /// </summary>
        public float? BoilingPoint { get => boilingPoint; set => boilingPoint = value; }

        //[WareSeacheable("Volatile","volatile")]
        ///// <summary>
        ///// 
        ///// </summary>
        //public bool? IsVolatile { get => @volatile; }


    }
}
