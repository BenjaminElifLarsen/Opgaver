using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystemCore
{
    public class Menu
    {
        /// <summary>
        /// Runs the main menu of the storage system.
        /// </summary>
        public void MainMenu() //put WareCreateMenu and WareChangeMenu into a sub menu. 
        {
            string title = "Main Menu";
            string[] menuOptions = new string[] {"Basic Storage View", "Expanded Storage View", "Add Ware","Change Ware", "Exit", /*" SQL Test"*/ };
            do
            {
                byte answer = Visual.MenuRun(menuOptions, title);
                switch (answer)
                {
                    case 0:
                        WareViewBasicMenu();
                        break;

                    case 2:
                        WareCreateMenu();
                        break;

                    case 3:
                        WareChangeMenu();
                        break;

                    case 4:
                        Environment.Exit(0);
                        break;

                    case 1:
                        WareViewMenu();
                        break;

                    case 5:
                        DatabaseSelectionMenu();
                        break;
                }
            } while (true);

        }

        private void WareChangeMenu()
        {
            bool run = true;
            string title = "Ware Change Menu";
            string[] options = new string[] { "Remove Ware", "Add To Ware", "Remove From Ware", "Back" };
            do
            {
                byte response = Visual.MenuRun(options, title);
                switch (response)
                {
                    case 0:
                        WareRemoveMenu(); //these should loop the menu until "Back" is selected. 
                        break;

                    case 1:
                        WareAddAmountMenu();
                        break;

                    case 2:
                        WareRemoveAmountMenu();
                        break;

                    case 3:
                        run = false;
                        break;
                }
            } while (run);
        }

        private void WareRemoveMenu()
        {
            string ID = CollectID();
            if (Support.IDExist(ID))
                if(WareModifier.RemoveWare(ID))
                {
                    ProgressInformer("{0} was removed.", ID);
                }
                else
                {
                    ProgressInformer("{0} was not removed.", ID);
                }
            else
            {
                Console.WriteLine("ID does not exist");
                Support.WaitOnKeyInput();
            }

            void ProgressInformer(string message, string part)
            {
                Console.Clear();
                Console.WriteLine(message, part); //figure out how to do this better. 
                Support.WaitOnKeyInput();
            }
        }

        private void WareAddAmountMenu()
        {
            string ID = CollectID();
            if (Support.IDExist(ID))
                WareModifier.AddToWare(ID,CollectAmount());
            else
            {
                Console.WriteLine("ID does not exist");
                Support.WaitOnKeyInput();
            }
        }

        private void WareRemoveAmountMenu()
        {
            string ID = CollectID();
            if (Support.IDExist(ID)) 
                WareModifier.RemoveFromWare(ID, CollectAmount());
            else
            {
                Console.WriteLine("ID does not exist");
                Support.WaitOnKeyInput();
            }
        }

        /// <summary>
        /// Asks the user to enter an amount and returns it.
        /// </summary>
        /// <returns></returns>
        private int CollectAmount() 
        {
            return Support.CollectValue("Enter Amount");
        }

        /// <summary>
        /// Asks the user to enter an ID and returns it.
        /// </summary>
        /// <returns></returns>
        private string CollectID()
        {
            return Support.CollectString("Enter ID");
        }

        /// <summary>
        /// Starts the creation of a new ware.
        /// </summary>
        private void WareCreateMenu()
        {
            WareCreator creator = new WareCreator(Publisher.PubWare); //move to somewhere else, maybe have a static class which a function/property that sets the publisher.
            Publisher.PubWare.CreateWare();
        } 

        /// <summary>
        /// Runs the code to view the basic information of all wares.
        /// </summary>
        private void WareViewBasicMenu()
        {
            Visual.WareDisplay(WareInformation.GetWareInformation());
            Support.WaitOnKeyInput();
        }

        /// <summary>
        /// Runs the code that allows the user to view specific information of all wares.
        /// </summary>
        private void WareViewMenu() 
        {
            Console.Clear();
            List<string> searchAttributes = WareInformation.FindAllSearchableAttributesNames();
            searchAttributes.Add("Type");
            searchAttributes.Add("Done");
            List<string> selectedAttributes = new List<string>(); 
            byte selected;
            do 
            {
                selected = Visual.MenuRun(searchAttributes.ToArray(), "Select Attributes");
                if (selected != searchAttributes.Count - 1 && !selectedAttributes.Contains(searchAttributes[selected]))
                    selectedAttributes.Add(searchAttributes[selected]);
            } while (selected != searchAttributes.Count-1);
            List<Dictionary<string, object>> attributesAndValues;
            if (selectedAttributes.Count != 0) 
            { 
                attributesAndValues = WareInformation.GetWareInformation(selectedAttributes);
                Visual.WareDisplay(selectedAttributes, attributesAndValues);
            }
            //Testing purpose, not related to this function
            //SQLCode.ObjectSQLConversion.ObjectToSQL(WareInformation.Ware[WareInformation.Ware.Count-1]);
            //SQLCode.ObjectSQLConversion.ObjectToSQL(WareInformation.Ware[0]);
        }

        /// <summary>
        /// Function used to set database (or no database)...
        /// </summary>
        public void DatabaseSelectionMenu() //consider moving this to somewhere else.
        {
            string[] options = new string[] { "Window login Authentication", "SQL Server Authentication", "No SQL Database" };
            string[] sqlInfo = new string[4];
            string firstConnection = null;
            string connect = null;
            
            bool run = true;

            do
            {
                byte answer = Visual.MenuRun(options, "Database");
                switch (answer) //do this until they either connect or uses the non-sql database
                {
                    case 0:
                        sqlInfo[0] = Support.CollectString("Enter Servername");
                        sqlInfo[1] = Support.CollectString("Enter database"); //first ask if they want to create a database or enter one
                        if (DoesDatabaseExist())
                        {
                            try
                            {
                                firstConnection = SQLCode.SQLControl.CreateConnectionString(sqlInfo[0], "master");
                                run = !SQLCode.SQLControl.InitalitionOfDatabase(sqlInfo, firstConnection, true);
                                //create database
                            }
                            catch
                            {
                                run = true;
                            }
                        }
                        else
                            try
                            {
                                run = !SQLCode.SQLControl.CreateConnection(sqlInfo, true);
                            }
                            catch
                            {
                                run = true;
                            }//when they start the problem and selects sql, the first conenction should go to the master database, 
                                                                    //which then creates the actual database and then a second connection is established to this database which then creates the table(s) and columns. This final connection is kept 
                                                                    //should firstly ask if they want to initialise database creation. If yes connnect to the master else connect directly to the database, since it and its table(s) (their columns should also exist)
                        break;

                    case 1:
                        sqlInfo[0] = Support.CollectString("Enter Servername");
                        sqlInfo[1] = Support.CollectString("Enter SQL SA");
                        sqlInfo[2] = Support.HiddenText("Enter Password");
                        sqlInfo[3] = Support.CollectString("Enter database");
                        if (DoesDatabaseExist()) 
                        {
                            try
                            {
                                firstConnection = SQLCode.SQLControl.CreateConnectionString(sqlInfo[0], "master", sqlInfo[2], sqlInfo[3]);
                                run = !SQLCode.SQLControl.InitalitionOfDatabase(sqlInfo, firstConnection, false);
                            }
                            catch
                            {
                                run = true;
                            }
                        }
                        else
                            try { 
                                run = !SQLCode.SQLControl.CreateConnection(sqlInfo, false);
                            }
                            catch
                            {
                                run = true;
                            }

                        break;

                    case 2:
                        WareInformation.AddWareDefault();
                        break;
                }
                if (answer == options.Length - 1)
                    run = false;
                if(run == true)
                {
                    Console.Clear();
                    Console.WriteLine("Could not establish connection to the database");
                    Support.WaitOnKeyInput();
                }

            } while (run);
        }

        
        /// <summary>
        /// Small menu that asks the user if the database exist or not.
        /// </summary>
        /// <returns>Returns true if the user selects yes to the database exist, else false.</returns>
        private bool DoesDatabaseExist() //rename
        {
            string[] options = new string[] {"Yes","No" };
            byte answer = Visual.MenuRun(options, "Initialise Database Creation?");
            return answer == 0;
        }


    }
}
