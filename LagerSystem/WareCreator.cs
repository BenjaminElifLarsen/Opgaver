using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            string title = "Ware Creation Menu";
            string[] options = new string[] { "Name", "ID", "Type", "Amount", "Finalise", "Back" }; //if Back is select and Finalise the program should ask for confirmation.
            do
            {
                byte answer = Visual.MenuRun(options, title);
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
                                WareInformation.AddWareTEst(name,ID,type,(uint)amount);
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
        private bool Confirmation() //move into Support
        {
            string message = "Are you sure?";
            byte response = Visual.MenuRun(new string[] {"Yes","No" }, message);
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
            Console.WriteLine("Enter Valid Product ID"); 
            Support.ActiveCursor();
            do
            {
                do
                {
                    ID_ = Console.ReadLine().Trim(); 
                } while (!ValidID(ID_));
            } while (!UniqueID(ID_)); 
            Support.DeactiveCursor();
            return ID_;
        }

        private bool UniqueID(string IDToCheck)
        {
            if (!Support.UniqueID(IDToCheck))
            {
                Console.WriteLine("ID is not unique. Enter a new ID");
                return false;
            }
            return true;
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
            string[] possibleTypes = FindWareTypes().ToArray(); //should handle an empty list

            return possibleTypes[0];
            //throw new NotImplementedException(); //use reflection to find all types.
        }

        private List<string> FindWareTypes()
        {
            List<string> typeList = new List<string>();
            Type[] types = Assembly.GetExecutingAssembly().GetTypes(); //finds all types in the executing assembly
            foreach (Type type in types)
            {
                List<Attribute> attrs = type.GetCustomAttributes().ToList(); //converts their custom attributes to a list
                if(!type.IsAbstract) //ensures the base class is not a "valid" type since it is abstract
                    foreach (Attribute attr in attrs) 
                        if (attr is WareTypeAttribute info) //is the attribute the correct one
                        {
                            typeList.Add(info.Type); //add to list
                            break;
                        }
                }
            return typeList;
        }


        protected void CreateWareEventHandler(object sender, ControlEvents.CreateWareEventArgs e) 
        {
            CreateWare();
        }

        private void RemoveFromSubscription(WarePublisher warePublisher)
        {
            warePublisher.RaiseCreateWareEvent -= CreateWareEventHandler;
        }

    }
}
