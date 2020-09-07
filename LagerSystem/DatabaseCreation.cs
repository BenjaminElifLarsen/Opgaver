using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    static class DatabaseCreation
    {
        public static void CreateDockerDatabase()
        {
            PowerShell ps = PowerShell.Create(); //needs to either add the database part of the script into the image as a linux bash script or make Powershell call into the SQL after creating the container and run the codes.
            ps.AddScript("SQLDatabase.ps1").Invoke(); //also, will not need the -it 
        }
    }
}
