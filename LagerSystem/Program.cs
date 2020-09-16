using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{

    //Krav

    //Det skal være muligt at indsætte objekter
    //    Objekterne skal som minimum have et id, et navn og et antal
    //    Objekterne må gerne indgå i et arvehirarki
    //Det skal være muligt at indsætte, ændre og slette vare i systemet
    //Det skal være muligt at se en liste over varer i databasen
    //Det er ikke nødvendigt at gemme i filer eller databaser

    class Program //move all of the different classes into their own files
    {
        static void Main(string[] args) //needs a system to ensure all IDs are unique
        {
            //DatabaseCreation.CreateDockerDatabase(); //needs to inform the user that they will need to run the powershell script before running the program if they want to use the SQL database
            
            dynamic test = Support.GetDefaultValueFromValueType("Int32");
            object test2 = Support.GetDefaultValueFromValueType("UInt32");
            object[] test3 = new object[] { test2, "testString" };
            object test4 = WareCreator.Test();
            Menu menu = new Menu();
            WareInformation.AddWareDefault();
            Input.RunInputThread();
            menu.MainMenu();
        }
    }

    
   
    

    






}
