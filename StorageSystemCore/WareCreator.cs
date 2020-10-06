using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystemCore
{
    public class WareCreator
    {
        private readonly WarePublisher warePublisher;
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
                            goBack = Support.Confirmation(); 
                            if (goBack)
                            {
                                type = Support.RemoveSpace(type);
                                if (SQLCode.SQLControl.DatabaseInUse)
                                {
                                    WareInformation.AddWare(name, ID, type, (int)amount);
                                    break;
                                }
                                object[] filledOutParameters = null;
                                if (ConstructorsExist(Type.GetType("StorageSystemCore." + type)))
                                    if(ExtraConstructorMenu())
                                    {
                                        string[] extraParameters = CreateSelectableConstructorList(Type.GetType("StorageSystemCore." + type));
                                        byte selectedCtor = SelectConstructor(extraParameters);
                                        filledOutParameters = ArquiringInformation(Type.GetType("StorageSystemCore." + type), selectedCtor);
                                        
                                    }
                                WareInformation.AddWare(name, ID, type, (int)amount, filledOutParameters);
                            }
                        }
                        break;

                    case 5:
                        goBack = Support.Confirmation();
                        break;
                }

            } while (!goBack);

            RemoveFromSubscription(warePublisher);
        }

        private byte SelectConstructor(string[] options)
        {
            Console.Clear();
            return Visual.MenuRun(options, "Select more information");
        }

        /// <summary>
        /// Creates a string array, where each entry contains a string with all non-base variable names, and returns it. 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string[] CreateSelectableConstructorList(Type type) 
        {
            List<List<string>> ctorsFromClass = WareInformation.FindConstructorsParameterNames(type);
            List<string> baseCtorVariables = WareInformation.BasicConstructorVariableNames;
            List<string> tempCtors = new List<string>();
            string[] ctorArray;
            for (int n = 0; n < ctorsFromClass.Count; n++)
            {
                tempCtors.Add("");
                for (int m = 0; m < ctorsFromClass[n].Count; m++)
                {
                    if (!baseCtorVariables.Contains(ctorsFromClass[n][m]))
                        tempCtors[tempCtors.Count - 1] += ctorsFromClass[n][m] + " ";
                }
            }
            tempCtors.RemoveAll(IsEmpty);
            ctorArray = tempCtors.ToArray();
            return ctorArray;

            bool IsEmpty(string str)
            {
                return str == null || str == "";
            }
        }


        private object[] ArquiringInformation(Type type, byte number)
        {
            Dictionary<string, Type> parameters = WareInformation.GetConstructorParameterNamesAndTypes(type,null)[number];
            object[] parameterValues = new object[parameters.Count];
            string[] parameterNames = parameters.Keys.ToArray();
            Type parameterType;

            for (int i = 0; i < parameterValues.Length; i++)
            {
                parameterType = parameters[parameterNames[i]];
                if (parameterType.IsValueType)
                {
                    var wareCreatorType = typeof(WareCreator);
                    var foundMethod = wareCreatorType.GetMethod("EnterExtraInformation", BindingFlags.NonPublic | BindingFlags.Static); 
                    var genericVersion = foundMethod.MakeGenericMethod(parameterType);
                    parameterValues[i] = genericVersion.Invoke(null, new object[] { parameterNames[i] });
                }
                else
                {
                    var test = typeof(WareCreator);
                    var test3 = test.GetMethod("EnterExtraInformation", BindingFlags.NonPublic | BindingFlags.Static);
                    var test2 = test3.MakeGenericMethod(parameterType);
                    parameterValues[i] = test2.Invoke(null, new object[] { parameterNames[i] }); 
                } 

            }
            return parameterValues;
        }

        /// <summary>
        /// Generic method to convert a string to a value type. Conversion for nullables does not work if the string contains a non-null value, rather the underlying type is returned. 
        /// </summary>
        /// <param name="primaryParameters"></param>
        /// <param name="secundaryParameters"></param>
        /// <returns></returns>
        private static t EnterExtraInformation<t>(string information) //need to catch cases where it cannot convert, e.g. converting "12q" to an int32. Also need to deal with an empty string (it should just return null
        {
            //https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.typeconverter?view=netcore-3.1
            Console.Clear();
            Console.WriteLine("Please Enter {0}",information);
            string value = Console.ReadLine();
            try
            {
                return (t)Convert.ChangeType(value, typeof(t)); 
            }
            catch
            {
                if (Nullable.GetUnderlyingType(typeof(t)) != null)
                {
                    try
                    {
                        return (t)Convert.ChangeType(value, Nullable.GetUnderlyingType(typeof(t)));
                    }
                    catch
                    {
                        throw new InvalidCastException();
                    }
                }
                else
                    throw new InvalidCastException();
            }
        }

        /// <summary>
        /// Asks if the user wants to input more information or not and returns true if they want too.
        /// </summary>
        /// <returns></returns>
        private bool ExtraConstructorMenu()
        {
            string title = "Do you want to add more information?";
            string[] options = new string[] {"Yes","No" };
            byte answer = Visual.MenuRun(options, title);
            return answer == 0;
        }

        private bool ConstructorsExist(Type type)
        {
            return WareInformation.FindConstructorsParameterNames(type).Count > 1;
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
            string[] possibleTypes = WareInformation.FindWareTypes().ToArray(); //should handle an empty list

            return possibleTypes[Visual.MenuRun(possibleTypes,"Select Type")];
            //throw new NotImplementedException(); //use reflection to find all types.
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
