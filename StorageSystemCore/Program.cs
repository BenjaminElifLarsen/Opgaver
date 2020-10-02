﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystemCore
{

    //Krav

    //Det skal være muligt at indsætte objekter
    //    Objekterne skal som minimum have et id, et navn og et antal
    //    Objekterne må gerne indgå i et arvehirarki
    //Det skal være muligt at indsætte, ændre og slette vare i systemet
    //Det skal være muligt at se en liste over varer i databasen
    //Det er ikke nødvendigt at gemme i filer eller databaser

    class Program 
    {
        static void Main(string[] args) 
        {
            //DatabaseCreation.CreateDockerDatabase(); //needs to inform the user that they will need to run the powershell script before running the program if they want to use the SQL database
            
            //dynamic test = Support.GetDefaultValueFromValueType("Int32");
            //object test2 = Support.GetDefaultValueFromValueType("UInt32");
            //object[] test3 = new object[] { test2, "testString" };
            //object test4 = Test(test2);//WareCreator.Test();
            Menu menu = new Menu();
            Input.RunInputThread();
            SQLCode.StoredProcedures.CreateAllStoredProcedures();
            menu.SQLTest();
            //WareInformation.AddWareDefault();
            menu.MainMenu();
        }

        //uses to be version controlled using the master branch and some other branches, however, since all projects uses the master branch it has been decided to move this project until its own "master" 
        //LS-Master and then all future branches will be called LS-{name}. No better time to start with git died and lost the master branch on the local repo.

    }

    
   
    

    






}
