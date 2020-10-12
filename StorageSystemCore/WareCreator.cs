using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystemCore
{
    sealed public class WareCreator
    {
        private readonly WarePublisher warePublisher;
        private WareCreator() { }
        public WareCreator(WarePublisher warePublisher)
        {
            warePublisher.RaiseCreateWareEvent += CreateWareEventHandler;
            this.warePublisher = warePublisher;
        }

        private void CreateWare() //at some point, either after all information has been added or after each, ask if it/they is/are correct and if they want to reenter information.
        {

            string ID = null;
            string name = null;
            string type = null;
            int? amount = null;
            bool goBack = false;
            string title = "Ware Creation Menu";
            string[] options = new string[] { "Name", "ID", "Type", "Amount", "Finalise", "Back" }; 
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
                        byte missingValues;
                        if (!MissingInformation(ID, name, type, amount, out missingValues)) //put this if/if-else-statment and its content into a function
                        {
                            goBack = Support.Confirmation(); 
                            if (goBack)
                            {
                                if (SQLCode.SQLControl.DatabaseInUse)
                                {
                                    Dictionary<string,Type> propertyNamesAndTypes = FindSQLProperties(Type.GetType("StorageSystemCore." + Support.RemoveSpace(type)));
                                    if (propertyNamesAndTypes.Count > 0)
                                        if(Visual.MenuRun(new string[] {"Yes","No" },"Do you want to enter extra information?") == 0)//will be moved into methods when finalised the code
                                        {
                                            List<string> selectedOptions = new List<string>(); //perhaps the displayed names should be Name rather than SQLName and later find the property with the attribute with Name and get its SQLName
                                            List<string> sqlOptions = propertyNamesAndTypes.Keys.ToList(); //find a better option for collection, at some point. 
                                            sqlOptions.Add("Done");
                                            byte selected;
                                            do
                                            {
                                                selected = Visual.MenuRun(sqlOptions.ToArray(), "Select information to add");
                                                if (selected != sqlOptions.Count - 1)
                                                    if (!selectedOptions.Contains(sqlOptions[selected]))
                                                        selectedOptions.Add(sqlOptions[selected]);
                                            } while (selected != sqlOptions.Count - 1);
                                            if(selectedOptions.Count > 0)
                                            {
                                                Dictionary<string,object> columnsAndValues = ArquiringInformation(Type.GetType("StorageSystemCore." + Support.RemoveSpace(type)), selectedOptions, propertyNamesAndTypes);
                                                WareInformation.AddWare(name, ID, type, (int)amount, columnsAndValues);
                                            }
                                        }
                                    else
                                        WareInformation.AddWare(name, ID, type, (int)amount);
                                    break;
                                }
                                //only if not using a database
                                object[] filledOutParameters = null;
                                type = Support.RemoveSpace(type);
                                if (ConstructorsExist(Type.GetType("StorageSystemCore." + type))) //Does multiple constructor exist?
                                    if(ExtraConstructorMenu())  //asks if the user wants to input more information
                                    {
                                        string[] extraParameters = CreateSelectableConstructorList(Type.GetType("StorageSystemCore." + type));
                                        byte selectedCtor = SelectConstructor(extraParameters); 
                                        filledOutParameters = ArquiringInformation(Type.GetType("StorageSystemCore." + type), selectedCtor);
                                        
                                    }
                                WareInformation.AddWare(name, ID, type, (int)amount, filledOutParameters);
                            }
                        }
                        else //informs the user of missing values. 
                        {
                            PrintOutMissingInformation(missingValues);
                            Support.WaitOnKeyInput();
                        }
                        break;

                    case 5:
                        goBack = Support.Confirmation();
                        break;
                }

            } while (!goBack);

            //RemoveFromSubscription(warePublisher);
        }

        /// <summary>
        /// Prints out the missing information.
        /// </summary>
        /// <param name="missingValues">'Binary flag' that contains the information about missing ware information.</param>
        private void PrintOutMissingInformation(byte missingValues)
        {
            string baseMessage = "The following is missing: {0}";
            string missingInfo = "";
            Console.WriteLine(Convert.ToString(missingValues, toBase: 2));
            if ((missingValues & 0b_0000_0001) == 0b_0000_0001)
                missingInfo += "ID ";
            if ((missingValues & 0b_0000_0010) == 0b_0000_0010)
                missingInfo += "Name ";
            if ((missingValues & 0b_0000_0100) == 0b_0000_0100)
                missingInfo += "Type ";
            if ((missingValues & 0b_0000_1000) == 0b_0000_1000)
                missingInfo += "Amount ";
            Console.Clear();
            Console.WriteLine(baseMessage, missingInfo);
        }

        /// <summary>
        /// Asks the user to select a constructor from <paramref name="options"/>.
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Returns the byte that belongs to the entry in <paramref name="options"/> that was selected. </returns>
        private byte SelectConstructor(string[] options)
        {
            Console.Clear();
            return Visual.MenuRun(options, "Select more information");
        }

        /// <summary>
        /// Creates a string array, where each entry contains a string with all non-base variable names, and returns it. 
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Returns a string array. Each index consist of a string with the names of all parameters of a specific constructor.</returns>
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

        /// <summary>
        /// Asks the user to import the values of the constructor from WareInformation.GetConstructorParameterNamesAndTypes with index of <paramref name="number"/> with the type of <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type of the ware whichs constructor(s)s should be found</param>
        /// <param name="number">The index of the selected constructor.</param>
        /// <returns></returns>
        private object[] ArquiringInformation(Type type, byte number)
        {
            Dictionary<string, Type> parameters = WareInformation.GetConstructorParameterNamesAndTypes(type,null)[number]; //consider replacing number and type with Dictionary<string, Type> and let the caller pass WareInformation.GetConstructorParameterNamesAndTypes(type,null)[number] as an arugument 
            object[] parameterValues = new object[parameters.Count];
            string[] parameterNames = parameters.Keys.ToArray();
            Type parameterType;
            Support.ActiveCursor();
            for (int i = 0; i < parameterValues.Length; i++)
            {
                parameterType = parameters[parameterNames[i]];
                if (parameterType.IsValueType)
                {
                    Type wareCreatorType = typeof(WareCreator);
                    MethodInfo foundMethod = wareCreatorType.GetMethod("EnterExtraInformation", BindingFlags.NonPublic | BindingFlags.Static);
                    MethodInfo genericVersion = foundMethod.MakeGenericMethod(parameterType);
                    try 
                    { 
                        parameterValues[i] = genericVersion.Invoke(null, new object[] { parameterNames[i] });
                    }
                    catch (Exception e)
                    {
                        Reporter.Report(e);
                        Console.Clear();
                        Console.WriteLine("Could not convert. Value set to 0: " + e.InnerException.Message);
                        parameterValues[i] = 0; //figure out a good way to reenter value
                        Support.WaitOnKeyInput();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please Enter {0}", parameterNames[i]);
                    parameterValues[i] = Console.ReadLine(); 
                } 

            }
            Support.DeactiveCursor();
            return parameterValues;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sqlColumns"></param>
        /// <param name="keyValuePairs"></param>
        private Dictionary<string,object> ArquiringInformation(Type type, List<string> sqlColumns, Dictionary<string,Type> keyValuePairs)
        {
            Support.ActiveCursor();
            Dictionary<string, object> nameAndValues = new Dictionary<string, object>();
            PropertyInfo[] propertyInfoes = type.GetProperties();
            foreach(PropertyInfo propertyInfo in propertyInfoes)
            {//write a Array.Contain(string) in Support at some point
                foreach (Attribute attre in propertyInfo.GetCustomAttributes())
                    if (attre is WareSeacheableAttribute info)
                    {
                        if (sqlColumns.Contains(info.SQLName))
                        {
                            if (keyValuePairs[info.SQLName].IsValueType) 
                            { 
                                Type wareCreatorType = typeof(WareCreator);
                                MethodInfo foundMethod = wareCreatorType.GetMethod("EnterExtraInformation", BindingFlags.NonPublic | BindingFlags.Static);
                                MethodInfo genericVersion = foundMethod.MakeGenericMethod(keyValuePairs[info.SQLName]);
                                try { 
                                    nameAndValues.Add(info.SQLName, genericVersion.Invoke(null, new object[] { info.Name }));
                                }
                                catch (Exception e)
                                {
                                    Reporter.Report(e);
                                    Console.Clear();
                                    Console.WriteLine("Could not convert. Value set to 0: " + e.InnerException.Message);
                                    nameAndValues.Add(info.SQLName, 0); //figure out a good way to reenter value
                                    Support.WaitOnKeyInput();
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Please Enter {0}", info.Name);
                                string value = Console.ReadLine();
                                nameAndValues.Add(info.SQLName, value);
                            }
                        }
                    }
            }
            Support.DeactiveCursor();
            return nameAndValues;
        }

        /// <summary>
        /// Generic method to convert a string to a value type. Conversion for nullables does not work if the string contains a non-null value, rather the underlying type is returned. 
        /// </summary>
        /// <param name="primaryParameters"></param>
        /// <param name="secundaryParameters"></param>
        /// <returns></returns>
        private static t EnterExtraInformation<t>(string information) 
        {
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
                else //cannot call this method from here, since it is called using reflection
                    throw new InvalidCastException();
            }
        }

        /// <summary>
        /// Asks if the user wants to input more information or not. Returns true if they want too.
        /// </summary>
        /// <returns>Returns true if the user wants to input more information else false.</returns>
        private bool ExtraConstructorMenu()
        {
            string title = "Do you want to add more information?";
            string[] options = new string[] {"Yes","No" };
            byte answer = Visual.MenuRun(options, title);
            return answer == 0;
        }

        /// <summary>
        /// Checks if <paramref name="type"/> contains more than one constructor. 
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>Returns true if more than 1 constructor exist, else false.</returns>
        private bool ConstructorsExist(Type type)
        {
            return WareInformation.FindConstructorsParameterNames(type).Count > 1;
        }

        /// <summary>
        /// Under development: Purpose is to act as the "select constructor, add name and type of its "parameters"" functions version for the sql database.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Dictionary<string,Type> FindSQLProperties(Type type) 
        { 
            Dictionary<string, Type> namesAndValues = new Dictionary<string, Type>();
            PropertyInfo[] propertyInfos = type.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
                    foreach (Attribute attre in propertyInfo.GetCustomAttributes())
                        if (attre is WareSeacheableAttribute info)
                        {
                            if (!WareInformation.BasicSQLNames.Contains(info.SQLName)) { 
                                Type propertyType = propertyInfo.GetMethod.ReturnType;
                                if (Nullable.GetUnderlyingType(propertyType) != null)
                                {
                                    string typeString = propertyType.FullName.Split(',')[0].Split("[[")[1]; //converts the nullable fullname to a string that can be used in Type.GetType(string).
                                    propertyType = Type.GetType(typeString);
                                }
                                namesAndValues.Add(info.SQLName, propertyType);
                        }
                    }
            return namesAndValues;
        }

        /// <summary>
        /// Checks if any of the parameters are null. Returns true if information is missing, else false.
        /// If any inforamtion is missing returns a combined binary value, <paramref name="missingValue"/>, that indicates which values are missing:
        /// 0000_0001 = <paramref name="id"/>,
        /// 0000_0010 = <paramref name="name"/>,
        /// 0000_0100 = <paramref name="type"/>,
        /// 0000_1000 = <paramref name="amount"/>. 
        /// </summary>
        /// <param name="id">ID to check</param>
        /// <param name="name">Name to check</param>
        /// <param name="type">Type to check</param>
        /// <param name="amount">Amount to check</param>
        /// <param name="missingValue">A "binary flag" that indicates which values are missing.</param>
        /// <returns>Returns true if any information is missing, else false.</returns>
        private bool MissingInformation(string id, string name, string type, int? amount, out byte missingValue)
        {
            missingValue = 0b_0000_0000;
            if (id == null)
                missingValue = (byte)(missingValue ^ 0b_0000_0001);
            if (name == null)
                missingValue = (byte)(missingValue ^ 0b_0000_0010);
            if (type == null)
                missingValue = (byte)(missingValue ^ 0b_0000_0100);
            if (amount == null)
                missingValue = (byte)(missingValue ^ 0b_0000_1000);
            if (missingValue == 0)
                return false;
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Collects and returns the name of the ware.
        /// </summary>
        /// <param name="message">The message to display for the user</param>
        /// <returns>Returns the name of the ware. </returns>
        private string EnterName(string message = "Enter Product Name")
        {
            return Support.CollectString(message);
        }

        /// <summary>
        /// Collects and returns the amount of the ware.
        /// </summary>
        /// <param name="message">The message to display for the user</param>
        /// <returns>Returns the amount of the ware.</returns>
        private int EnterAmount(string message = "Enter Amount")
        {
            return Support.CollectValue(message);
        }

        /// <summary>
        /// Allows the user to create a new ID while ensuring the ID is valid and unique. 
        /// </summary>
        /// <returns>Returns an unique ID for the ware. </returns>
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
            if (!Support.UniqueID(IDToCheck, SQLCode.SQLControl.DatabaseInUse))
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

        /// <summary>
        /// Allows for the selection of a ware type.
        /// </summary>
        /// <returns></returns>
        private string SelectType()
        {
            string[] possibleTypes = WareInformation.FindWareTypes().ToArray(); //should handle an empty list

            return possibleTypes[Visual.MenuRun(possibleTypes,"Select Type")];
        }

        /// <summary>
        /// Subscribes the a specific function, CreateWare(), of the class instance to an event for ware creation. 
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The parameters of the event</param>
        private void CreateWareEventHandler(object sender, ControlEvents.CreateWareEventArgs e) 
        {
            CreateWare();
        }

        /// <summary>
        /// Unsubscribes the class from the ware creation event.  
        /// </summary>
        /// <param name="warePublisher"></param>
        private void RemoveFromSubscription(WarePublisher warePublisher)
        {
            warePublisher.RaiseCreateWareEvent -= CreateWareEventHandler;
        }

    }
}
