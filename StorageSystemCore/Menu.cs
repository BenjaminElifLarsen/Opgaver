using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    public class Menu
    {
        /// <summary>
        /// Runs the main menu of the storage system.
        /// </summary>
        public void MainMenu() //put WareCreateMenu and WareChangeMenu into a sub menu. 
        {
            string title = "Main Menu";
            string[] menuOptions = new string[] {"Basic Storage", "Expanded Storage", "Add Ware","Change Ware", "Exit", /*" SQL Test"*/ };
            do
            {
                byte answer = Visual.MenuRun(menuOptions, title);
                switch (answer)
                {
                    case 0:
                        WareViewAllMenu();
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
                        SQLTest();
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

        private void WareCreateMenu()
        {
            WareCreator creator = new WareCreator(Publisher.PubWare); //move to somewhere else
            Publisher.PubWare.CreateWare();
        } 

        private void WareViewAllMenu()
        {
            Visual.WareDisplay(WareInformation.GetWareInformation());
            Support.WaitOnKeyInput();
        }

        private void WareViewMenu() 
        {
            Console.Clear();
            List<string> searchAttributes = WareInformation.FindAllSearchableAttributesNames();
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
                attributesAndValues = WareInformation.GetWareInformation(selectedAttributes); /*new string[] { "Name", "Amount", "Information" }.ToList()*/
        }

        public void SQLTest() //consider moving this to somewhere else.
        {
            string[] options = new string[] { "Window login Authentication", "SQL Server Authentication", "No SQL Database" };
            byte answer = Visual.MenuRun(options, "Database");
            string[] sqlInfo = new string[4];
            string connect;
            switch (answer)
            {
                case 0:
                    sqlInfo[0] = Support.CollectString("Enter Servername"); 
                    sqlInfo[1] = Support.CollectString("Enter database"); //first ask if they want to create a database or enter one
                    connect = SQLCode.SQLControl.CreateConnectionString(sqlInfo[0], sqlInfo[1]);
                    SQLCode.SQLControl.CreateConnection(connect);
                    break;

                case 1:
                    sqlInfo[0] = Support.CollectString("Enter Servername");
                    sqlInfo[1] = Support.CollectString("Enter SQL SA");
                    sqlInfo[2] = Support.HiddenText("Enter Password");
                    sqlInfo[3] = Support.CollectString("Enter database"); //first ask if they want to create a database or enter one
                    connect = SQLCode.SQLControl.CreateConnectionString(sqlInfo[0], sqlInfo[1], sqlInfo[2], sqlInfo[3]);
                    SQLCode.SQLControl.CreateConnection(connect);
                    break;

                case 2:
                    WareInformation.AddWareDefault();
                    break;
            }
        }


    }
}
