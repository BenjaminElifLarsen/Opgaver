using System;
using System.Collections.Generic;
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
        string name;
        string id;
        int amount; //should a negative unit amount be allowed? E.g. more units ordered than there are? 

        private Ware() { }

        public Ware(string name, string id, int amount, WarePublisher warePublisher)
        {
            this.name = name;
            this.id = id;
            this.amount = amount;
            warePublisher.RaiseAddEvent += AddAmountEventHandler;
            warePublisher.RaiseRemoveEvent += RemoveAmountEvnetHandler;
        }

        /// <summary>
        /// Gets the name of the ware.
        /// </summary>
        public string GetName { get => name; }

        /// <summary>
        /// Gets the amount of the ware.
        /// </summary>
        public int GetAmount { get => amount; }

        /// <summary>
        /// Gets the ID of the ware.
        /// </summary>
        public string GetID { get => id; }

        protected virtual void Add(int amount)
        {
            this.amount += amount;
        }

        protected virtual void Remove(int amount)
        {
            if(amount != 0)
                this.amount -= amount;
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
