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
        { //have a creation menu where the user can select which entry they want to enter when they want to and they can first finish when all entries have been entered. Also add a "back" option
            string ID = CreateID();
            string name = EnterName();
            uint amount = EnterAmount();
            Publisher.PubWare.CreateWare(name,ID,amount);
        }

        public string EnterName()
        {
            string name;
            Console.WriteLine("Enter Product Name");
            do
            {
                name = Console.ReadLine();
            } while (name == null || name == "");
            return name;
        }

        public uint EnterAmount()
        {
            uint value;
            string valueString;
            do
            {
                valueString = Console.ReadLine();
            } while (uint.TryParse(valueString, out value));
            return value;
        }

        public string CreateID()
        {
            string ID_;
            Console.WriteLine("Enter Product ID");
            do
            {
                ID_ = Console.ReadLine();
            } while (Support.UniqueID(ID_));

            return ID_;
        }


        protected void CreateWareEventHandler(object sender, ControlEvents.CreateWareEventArgs e) //instead of calling this here, call it in the main menu and change CreateWareEventArgs to not contain any parameters
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
