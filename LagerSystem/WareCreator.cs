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

        //static WareCreator() { Publisher.PubWare.RaiseCreateWareEvent += CreateWareEventHandler;}

        private void CreateWare() //at some point, either after all information has been added or after each, ask if it/they is/are correct and if they want to reenter information.
        { //have a creation menu where the user can select which entry they want to enter when they want to and they can first finish when all entries have been entered. Also add a "back" option

            string ID = null;
            string name = null;
            string type = null;
            int? amount = null;
            bool goBack = false;
            string title = "Ware Creation Menu";
            string[] options = new string[] { "Name", "ID", "Type", "Amount", "Finalise", "Back" }; //if Back is select and Finalise the program should ask for confirmation.
            //make a deep copy of the options and overwrite its value. Need to overwrite an entry before the value is added to an entry
            string[] displayOptions = Support.DeepCopy(options);
            do
            {
                byte answer = Visual.MenuRun(displayOptions, title);
                switch (answer)
                {
                    case 0:
                        name = EnterName();
                        displayOptions[0] = options[0];
                        displayOptions[0]  += ": " + name;
                        break;

                    case 1:
                        ID = CreateID();
                        displayOptions[1] = options[1];
                        displayOptions[1] += ": " + ID;
                        break;

                    case 2:
                        type = SelectType();
                        displayOptions[2] = options[2];
                        displayOptions[2] += ": " + type;
                        break;

                    case 3:
                        amount = EnterAmount();
                        displayOptions[3] = options[3];
                        displayOptions[3] += ": " + amount;
                        break;

                    case 4:
                        if (!MissingInformation(ID, name, type, amount)) //put this if-statment and its contect into a function
                        {
                            goBack = Support.Confirmation(); //if the type contains multiple constructors with extra parameters ask the user if they want to fill out more information 
                            if (goBack)//(they can select a specific constructor and then fill out the specific parameters that are extra). 
                            {
                                WareInformation.AddWare(name,ID,type,(int)amount);
                                if (type.Split(' ').Length != 1) //move into a function since it so far is needed two places
                                {
                                    string[] split = type.Split(' ');
                                    type = "";
                                    foreach (string typing in split)
                                        type += typing;
                                }
                                if(ConstructorsExist(Type.GetType("LagerSystem." + type)))
                                    if(ExtraConstructorMenu())
                                    {

                                    }
                                    else
                                        WareInformation.AddWare(name, ID, type, (int)amount);
                            }
                        }
                        break;

                    case 5:
                        goBack = Support.Confirmation();
                        //go back
                        break;
                }

            } while (!goBack);

            RemoveFromSubscription(warePublisher);
        }

        private bool ExtraConstructorMenu()
        {
            string title = "Do you want to add more information?";
            string[] options = new string[] {"Yes","No" };
            byte answer = Visual.MenuRun(options, title);
            return answer == 0;
        }

        private bool ConstructorsExist(Type type)
        {
            return WareInformation.FindConstructors(type).Count > 1;
            throw new NotImplementedException();
        }


        /// <summary>
        /// Checks if any of the parameters are void and prints out information about which are void. Returns false if no information is missing.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        private bool MissingInformation(string id, string name, string type, int? amount)
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
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string EnterName(string message = "Enter Product Name")
        {
            return Support.CollectString(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private int EnterAmount(string message = "Enter Amount")
        {
            return Support.CollectValue(message);
        }

        /// <summary>
        /// Allows the user to create a new ID while ensuring the ID is valid and unique. 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Checks if an ID is unique or not. If not unique it will informs the user about it.
        /// </summary>
        /// <param name="IDToCheck">ID to check.</param>
        /// <returns>Returns true if the ID is unique else false.</returns>
        private bool UniqueID(string IDToCheck)
        {
            if (!Support.UniqueID(IDToCheck))
            {
                Console.WriteLine("ID is not unique. Enter a new ID");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if <paramref name="IDToCheck"/> is valid.
        /// </summary>
        /// <param name="IDToCheck"></param>
        /// <returns></returns>
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

            return possibleTypes[Visual.MenuRun(possibleTypes,"Select Type")];
            //throw new NotImplementedException(); //use reflection to find all types.
        }

        private List<string> FindWareTypes() //move into Support
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
