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

        public void CreateWare() //at some point, either after all information has been added or after each, ask if it/they is/are correct and if they want to reenter information.
        {
            string ID = CreateID();
            string name = "";
            uint amount = 0;
            Publisher.PubWare.CreateWare(name,ID,amount);
        }

        public string CreateID()
        {
            Console.WriteLine("Enter ID");
            string ID_;
            do
            {
                ID_ = Console.ReadLine();
            } while (Support.UniqueID(ID_));

            throw new NotImplementedException();
        }


        protected void CreateWareEventHandler(object sender, ControlEvents.CreateWareEventArgs e)
        {
            Type type = Type.GetType(e.Type);
            if (type == Type.GetType("Liquids"))
                WareInformation.Ware.Add(new Liquids(e.Name, e.ID, e.Amount, warePublisher)); //needs to deal with different types, maybe just use polymorphy or a combination?
        }

        public void RemoveFromSubscription(WarePublisher warePublisher)
        {
            warePublisher.RaiseCreateWareEvent -= CreateWareEventHandler;
        }

    }
}
