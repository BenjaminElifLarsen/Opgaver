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
            PowerShell ps = PowerShell.Create();
            ps.AddScript("SQLDatabase.ps1").Invoke();
        }
    }
}
