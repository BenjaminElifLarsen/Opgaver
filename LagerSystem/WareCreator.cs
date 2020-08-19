using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    public class WareCreator
    {
        private WarePublisher warePublisher;
        private WareCreator() { }
        public WareCreator(WarePublisher warePublisher)
        {
            warePublisher.RaiseCreateWareEvent += CreateWareEventHandler;
            this.warePublisher = warePublisher;
        }

        protected void CreateWareEventHandler(object sender, ControlEvents.CreateWareEventArgs e)
        {
            Type type = Type.GetType(e.Type);
            if (type == Type.GetType("Liquids"))
                WareInformation.Ware.Add(new Liquids(e.Name, e.ID, e.Amount, warePublisher)); //needs to deal with different types, maybe just use polymorphy 
        }

        public void RemoveFromSubscription(WarePublisher warePublisher)
        {
            warePublisher.RaiseCreateWareEvent -= CreateWareEventHandler;
        }

    }
}
