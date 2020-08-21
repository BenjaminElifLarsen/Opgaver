using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    [WareType("None")] //Ware is abstract and cannot be initiolasedwskdlwd
    abstract class Ware
    {
        string name;
        string id;
        uint amount; //should a negative unit amount be allowed? E.g. more units ordered than there are? 

        public Ware(string name, string id, uint amount, WarePublisher warePublisher)
        {
            this.name = name;
            this.id = id;
            this.amount = amount;
            warePublisher.RaiseAddEvent += AddAmountEventHandler;
            warePublisher.RaiseRemoveEvent += RemoveAmountEvnetHandler;
        }

        public string GetName { get => name; }

        public uint GetAmount { get => amount; }

        public string GetID { get => id; }

        protected virtual void Add(uint amount)
        {
            this.amount += amount;
        }

        protected virtual void Remove(uint amount)
        {
            this.amount -= amount;
        }

        protected void AddAmountEventHandler(object sender, ControlEvents.AddEventArgs e)
        {
            if (e.ID == this.id)
                Add(e.AmountToAdd);
        }

        protected void RemoveAmountEvnetHandler(object sender, ControlEvents.RemoveEventArgs e)
        {
            if (e.ID == this.id)
                Remove(e.AmountToRemove);
        }

        public void RemoveSubscriptions(WarePublisher warePublisher)
        {
            warePublisher.RaiseAddEvent -= AddAmountEventHandler;
            warePublisher.RaiseRemoveEvent -= RemoveAmountEvnetHandler;
        }


    }
}
