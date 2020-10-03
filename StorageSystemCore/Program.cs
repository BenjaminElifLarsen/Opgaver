using System;
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
            Menu menu = new Menu();
            Input.RunInputThread();
            menu.DatabaseSelectionMenu();
            //SQLCode.StoredProcedures.CreateAllStoredProcedures();
            menu.MainMenu();
        }

        //uses to be version controlled using the master branch and some other branches, however, since all projects uses the master branch it has been decided to move this project until its own "master" 
        //LS-Master and then all future branches will be called LS-{name}. No better time to start with git died and lost the master branch on the local repo.

    }

    
   
    

    






}
