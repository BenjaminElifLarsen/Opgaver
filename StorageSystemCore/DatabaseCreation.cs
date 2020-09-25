using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystemCore
{
    static class DatabaseCreation
    {
        public static void CreateDockerDatabase()
        {
            //PowerShell ps = PowerShell.Create(); //needs to either add the database part of the script into the image as a linux bash script or make Powershell call into the SQL after creating the container and run the codes.
            
            //ps.AddScript("docker run -e \"ACCEPT_EULA = Y\" -e \"SA_PASSWORD = Password123.\" -p 1435:1433 --name SQLStorageSystem -d mcr.microsoft.com/mssql/server:2019-CU5-ubuntu-18.04").Invoke(); //also, will not need the -it 
            //ps = PowerShell.Create();
            //ps.AddScript("Start-Sleep -s 20").Invoke();
            //ps = PowerShell.Create();
            //ps.AddScript("sqlcmd -S 127.0.0.1,1435 -U SA -P \"Password123.\" -e -i DatabaseCreation.sql").Invoke();
            //ps = PowerShell.Create();
            //ps.AddScript("Start-Sleep -s 6").Invoke();
            ////var test = ps.Invoke();
        }
    }
}
