using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLCode
{
    static class DatabaseCreation
    {
        private static void CreateDatabase()
        {
            //have a read me file with mention the line below
            //"docker run -e \"ACCEPT_EULA = Y\" -e \"SA_PASSWORD = Password123.\" -p 1435:1433 --name SQLStorageSystem -d mcr.microsoft.com/mssql/server:2019-CU5-ubuntu-18.04").Invoke(); 

        }

        private static void CreateTable()
        {

        }

        private static void CreateDefaultEntries()
        {

        }

        public static void InitialiseDatabase()
        {
            CreateDatabase();
            CreateTable();
            CreateDefaultEntries();
        }
    }
}
