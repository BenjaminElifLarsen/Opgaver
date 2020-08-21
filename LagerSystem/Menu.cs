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
            string[] options = new string[] { "Remove Ware", "Add To Ware", "Remove From Ware" };
            Publisher.PubWare.RemoveFromWare("ID-55t",1);
            Publisher.PubWare.AddToWare("ID-55t2",1);

        }

        private void WareCreateMenu()
        {
            WareCreator creator = new WareCreator(Publisher.PubWare);
            Publisher.PubWare.CreateWare();
        } 

        private void WareViewAllMenu()
        {

            Visual.WareDisplay(WareInformation.GetWareInformation());
            Support.WaitOnKeyInput();
        }

        




    }
}
