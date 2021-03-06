﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    /// <summary>
    /// The basic ware class, abstract, that all different types of wares should inherence from.
    /// </summary>
    [WareType("None")] //Ware is abstract and cannot be initialisated
    abstract class Ware
    {
        protected string name;
        protected string id;
        protected int amount; //should a negative unit amount be allowed? E.g. more units ordered than there are? 
        protected string information = "Missing";

        private Ware() { }

        /// <summary>
        /// Default Ware constructor.
        /// </summary>
        /// <param name="name">The name of the ware.</param>
        /// <param name="id">The ID of the ware.</param>
        /// <param name="amount">The unit amount of the ware.</param>
        /// <param name="warePublisher">The ...</param>
        public Ware(string name, string id, int amount, WarePublisher warePublisher)
        {
            this.name = name;
            this.id = id;
            this.amount = amount;
            warePublisher.RaiseAddEvent += AddAmountEventHandler;
            warePublisher.RaiseRemoveEvent += RemoveAmountEvnetHandler;
        }

        //have attributes for constructors, if Type does not contain a way to find them
        /// <summary>
        /// Default Ware consturctor with product information added.
        /// </summary>
        /// <param name="name">The name of the ware.</param>
        /// <param name="id">The ID of the ware.</param>
        /// <param name="information">Information about the ware.</param>
        /// <param name="amount">The unit amount of the ware.</param>
        /// <param name="warePublisher">...</param>
        public Ware(string name, string id, string information, int amount, WarePublisher warePublisher) : this(name, id, amount, warePublisher)
        {
            this.information = information;
        }

        [WareSeacheable("Name")]
        /// <summary>
        /// Gets the name of the ware.
        /// </summary>
        public string GetName { get => name; }

        [WareSeacheable("Amount")]
        /// <summary>
        /// Gets the amount of the ware.
        /// </summary>
        public int GetAmount { get => amount; }

        [WareSeacheable("Information")]
        /// <summary>
        /// Gets the ware information
        /// </summary>
        public string GetInformation { get => information; }

        [WareSeacheable("ID")]
        /// <summary>
        /// Gets the ID of the ware.
        /// </summary>
        public string GetID { get => id; }

        /// <summary>
        /// Add the <paramref name="amount"/> to the unit amount of the ware.
        /// </summary>
        /// <param name="amount"></param>
        protected virtual void Add(int amount)
        {
            this.amount += amount;
        }

        /// <summary>
        /// Removes the <paramref name="amount"/> from the unit amount of the ware.
        /// </summary>
        /// <param name="amount"></param>
        protected virtual void Remove(int amount)
        {
                this.amount -= amount;
        }

        protected virtual void AddInformation(string info)
        {
            information = info;
        }

        /// <summary>
        /// Adds the amount of wares as specificed in <paramref name="e"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddAmountEventHandler(object sender, ControlEvents.AddEventArgs e)
        {
            if (e.ID == this.id)
                Add(e.AmountToAdd);
        }

        /// <summary>
        /// Removes the amount of wares as specificed in <paramref name="e"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RemoveAmountEvnetHandler(object sender, ControlEvents.RemoveEventArgs e)
        {
            if (e.ID == this.id)
                Remove(e.AmountToRemove);
        }

        /// <summary>
        /// Removes the subscriptions...
        /// </summary>
        /// <param name="warePublisher"></param>
        public void RemoveSubscriptions(WarePublisher warePublisher) //move this parameter into the class scope
        {
            warePublisher.RaiseAddEvent -= AddAmountEventHandler;
            warePublisher.RaiseRemoveEvent -= RemoveAmountEvnetHandler;
        }

    }
}
