using static System.Console;

namespace ConsoleApp
{
    class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                WriteLine("Usage: schema2fm.exe connectionstring schema table");
                return;
            }

            var connectionString = args[0];//@"Server=.\SQLEXPRESS;Database=LearnORM;Trusted_Connection=True;";
            var schemaName = args[1];//"dbo";
            var tableName = args[2];//"Book";

            var columnsProvider = new ColumnsProvider(connectionString);
            var columns = columnsProvider.GetColumnsAsync(schemaName, tableName).GetAwaiter().GetResult();

            new Table(tableName, columns).OutputMigrationCode(Out);
        }
    }
}
