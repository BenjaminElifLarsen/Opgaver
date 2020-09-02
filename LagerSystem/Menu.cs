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
            string[] menuOptions = new string[] {"Storage","Add Ware","Change Ware", "Exit" };
            do
            {
                byte answer = Visual.MenuRun(menuOptions, title);
                switch (answer)
                {
                    case 0:
                        WareViewAllMenu();
                        break;

                    case 1:
                        WareCreateMenu();
                        break;

                    case 2:
                        WareChangeMenu();
                        break;

                    case 3:
                        Environment.Exit(0);
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
        private int CollectAmount() //WareCreator.EnterAmount does what this should do, so consider moving the code of that function into a Support function and then call that function from here and WareCreator.EnterAmount
        {
            return Support.EnterAmount("Enter Amount");
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





    }
}
