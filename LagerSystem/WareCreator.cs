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

            string ID;
            string name;
            uint? amount;
            bool goBack = false;
            string title = "Ware Creation";
            string[] options = new string[] { "Name", "ID", "Type", "Amount", "Finalise", "Back" }; //if Back is select and Finalise the program should ask for confirmation.
            do
            {
                byte answer = Visual.MenuRun(options);
                switch (answer)
                {
                    case 0:
                        break;

                    case 1:
                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        break;

                    case 5:
                        break;
                }

            } while (!goBack);

            ID = CreateID();
            name = EnterName();
            amount = EnterAmount();
        }



        private string EnterName()
        {
            string name;
            Console.WriteLine("Enter Product Name");
            do
            {
                name = Console.ReadLine();
            } while (name == null || name == "");
            return name;
        }

        private uint EnterAmount()
        {
            uint value;
            string valueString;
            do
            {
                valueString = Console.ReadLine();
            } while (uint.TryParse(valueString, out value));
            return value;
        }

        private string CreateID()
        {
            string ID_;
            Console.WriteLine("Enter Valid Product ID"); //have requirements for the ID and use REGEX to check after, e.g. min. 1 letter, 1 number, min size of 6 and maximum size of 20
            do
            {
                do
                {
                    ID_ = Console.ReadLine();
                } while (ValidID(ID_));
            } while (Support.UniqueID(ID_));

            return ID_;
        }

        private bool ValidID(string IDToCheck)
        {
            throw new NotImplementedException();
        }

        private string SelectType()
        {
            throw new NotImplementedException(); //use reflection to find all types.
        }


        protected void CreateWareEventHandler(object sender, ControlEvents.CreateWareEventArgs e) //instead of calling this here, call it in the main menu and change CreateWareEventArgs to not contain any parameters
        {
            //Type type = Type.GetType(e.Type);
            //if (type == Type.GetType("Liquids"))
            //    WareInformation.Ware.Add(new Liquids(e.Name, e.ID, e.Amount, warePublisher)); //needs to deal with different types, maybe just use polymorphy or a combination?
        }

        public void RemoveFromSubscription(WarePublisher warePublisher)
        {
            warePublisher.RaiseCreateWareEvent -= CreateWareEventHandler;
        }

    }
}
