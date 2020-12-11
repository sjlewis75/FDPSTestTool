using System.Threading;
using Database;


namespace FDPSTestingTool
{
    class DatabaseFunctions
    {
        public static void TruncateFdpsTables()
        {
            ExecuteDatabaseFile("TruncateTables.txt");
        }

        public static void CreateFdpsSchema()
        {
            ExecuteDatabaseFile("CreateSchema.txt");
        }

        public static void DropFdpsSchema()
        {
            ExecuteDatabaseFile("DropSchema.txt");

        }

        public static int CreateFdpsDb()
        {

            var connection = DatabaseCommands.ConnectToDb();
            string sqlCmd;

            System.IO.StreamReader file = new System.IO.StreamReader("../../../CreateDatabase.txt");
            sqlCmd = file.ReadLine();
            int dbExists = DatabaseCommands.ExecuteRowCountCommand(sqlCmd, connection);
            if (dbExists != 1)
            { 
                sqlCmd = file.ReadLine();
                DatabaseCommands.RunNonQueryCommand(sqlCmd, connection);
                Thread.Sleep(10);
                dbExists = 0;
            }
            else
            {
                dbExists = 1;
            }

            DatabaseCommands.DisconnectFromDb(connection);
            return dbExists;
        }

        public static void DropFdpsDb()
        {           
            ExecuteDatabaseFile("DropDatabase.txt");
        }

        private static void ExecuteDatabaseFile(string fileToExecute)
        {
            var connection = DatabaseCommands.ConnectToDb();
            string sqlCmd;

            System.IO.StreamReader file = new System.IO.StreamReader("../../../" + fileToExecute);
            while ((sqlCmd = file.ReadLine()) != null)
            {
                DatabaseCommands.RunNonQueryCommand(sqlCmd, connection);
            }

            DatabaseCommands.DisconnectFromDb(connection);
        }
    }
}
