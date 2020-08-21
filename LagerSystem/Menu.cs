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
        public void MainMenu()
        {
            string[] menuOptions = new string[] {"Storage","Add Ware","Change Ware", "Exit" };
            do
            {
                byte answer = Visual.MenuRun(menuOptions);
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
            string[] options = new string[] { "Remove Ware", "Add To Ware", "Remove From Ware", "Back" };
            Publisher.PubWare.RemoveFromWare("ID-55t",1);
            Publisher.PubWare.AddToWare("ID-55t2",1);
            byte response = Visual.MenuRun(options);
            switch (response)
            {
                case 0:
                    WareRemoveMenu();
                    break;

                case 1:
                    WareAddAmountMenu();
                    break;

                case 2:
                    WareRemoveAmountMenu();
                    break;
            }
        }

        private void WareRemoveMenu()
        {
            WareModifier.RemoveWare(CollectID());
        }

        private void WareAddAmountMenu()
        {
            WareModifier.AddToWare(CollectID(),CollectAmount());
        }

        private void WareRemoveAmountMenu()
        {
            WareModifier.RemoveFromWare(CollectID(), CollectAmount());
        }

        private uint CollectAmount() //WareCreator.EnterAmount does what this should do, so consider moving the code of that function into a Support function and then call that function from here and WareCreator.EnterAmount
        {
            throw new NotImplementedException();
        }

        private string CollectID()
        {
            throw new NotImplementedException();
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
