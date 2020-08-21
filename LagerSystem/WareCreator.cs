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

        private void CreateWare() //at some point, either after all information has been added or after each, ask if it/they is/are correct and if they want to reenter information.
        { //have a creation menu where the user can select which entry they want to enter when they want to and they can first finish when all entries have been entered. Also add a "back" option

            string ID = null;
            string name = null;
            string type = null;
            uint? amount = null;
            bool goBack = false;
            string title = "Ware Creation";
            string[] options = new string[] { "Name", "ID", "Type", "Amount", "Finalise", "Back" }; //if Back is select and Finalise the program should ask for confirmation.
            do
            {
                byte answer = Visual.MenuRun(options);
                switch (answer)
                {
                    case 0:
                        name = EnterName();
                        options[0]  += ": " + name;
                        break;

                    case 1:
                        ID = CreateID();
                        options[1] += ": " + ID;
                        break;

                    case 2:
                        type = SelectType();
                        options[2] += ": " + type;
                        break;

                    case 3:
                        amount = EnterAmount();
                        options[3] += ": " + amount;
                        break;

                    case 4:
                        if (!MissingInformation(ID, name, type, amount)) //put this if-statment and its contect into a function
                        {
                            goBack = Confirmation();
                            if (goBack)
                            {
                                //create ware
                            }
                        }
                        break;

                    case 5:
                        goBack = Confirmation();
                        //go back
                        break;
                }

            } while (!goBack);

            RemoveFromSubscription(warePublisher);
        }

        /// <summary>
        /// Checks if any of the parameters are void and prints out information about which are void. Returns false if no information is missing.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        private bool MissingInformation(string id, string name, string type, uint? amount)
        {
            string baseMessage = "The following is missing: ";
            string missing = baseMessage;
            if (id == null)
                missing += "ID ";
            if (name == null)
                missing += "Name ";
            if (type == null)
                missing += "Type ";
            if (amount == null)
                missing += "Amount ";
            if (baseMessage == missing)
                return false;
            else
            {
                Console.Clear();
                Console.WriteLine(missing);
                Support.WaitOnKeyInput();
                return true;
            }
        }

        /// <summary>
        /// Asks if an user is sure about their choice. If yes it returns true, else false.
        /// </summary>
        /// <returns></returns>
        private bool Confirmation()
        {
            string message = "Are you sure?";
            byte response = Visual.MenuRun(new string[] {"Yes","No" });
            return response == 0 ? true : false;
        }

        private string EnterName(string message = "Enter Product Name")
        {
            return Support.CollectString(message);
        }

        private uint EnterAmount(string message = "Enter Amount")
        {
            return Support.EnterAmount(message);
        }

        private string CreateID()
        {
            string ID_;
            Console.Clear();
            Console.WriteLine("Enter Valid Product ID"); //have requirements for the ID and use REGEX to check after, e.g. min. 1 letter, 1 number, min size of 4 and maximum size of 18
            Support.ActiveCursor();
            do
            {
                do
                {
                    ID_ = Console.ReadLine().Trim();
                } while (!ValidID(ID_));
            } while (!Support.UniqueID(ID_));
            Support.DeactiveCursor();
            return ID_;
        }

        private bool ValidID(string IDToCheck)
        {
            if (!RegexControl.IsValidLength(IDToCheck))
            {
                Console.WriteLine("Invalid: Wrong Length, min = 6, max = 16");
                return false;
            }
            if (!RegexControl.IsValidValues(IDToCheck))
            {
                Console.WriteLine("Invalid: No numbers");
                return false;
            }
            if (!RegexControl.IsValidLettersLower(IDToCheck))
            {
                Console.WriteLine("Invalid: No lowercase letters");
                return false;
            }
            if (!RegexControl.IsValidLettersUpper(IDToCheck))
            {
                Console.WriteLine("Invalid: No uppercase letters");
                return false;
            }
            if(!RegexControl.IsValidSpecial(IDToCheck))
            {
                Console.WriteLine("Invalid: No special symbols: {0}", RegexControl.GetSpecialSigns);
                return false;
            }
            return true;
        }

        private string SelectType()
        {
            throw new NotImplementedException(); //use reflection to find all types.
        }


        protected void CreateWareEventHandler(object sender, ControlEvents.CreateWareEventArgs e) 
        {
            CreateWare();
            //Type type = Type.GetType(e.Type);
            //if (type == Type.GetType("Liquids"))
            //    WareInformation.Ware.Add(new Liquids(e.Name, e.ID, e.Amount, warePublisher)); //needs to deal with different types, maybe just use polymorphy or a combination?
        }

        private void RemoveFromSubscription(WarePublisher warePublisher)
        {
            warePublisher.RaiseCreateWareEvent -= CreateWareEventHandler;
        }

    }
}
