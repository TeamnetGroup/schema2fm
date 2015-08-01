using static System.Console;

namespace ConsoleApp
{
    class Program
    {
        private static void Main()
        {
            var connectionString = @"Server=.\SQLEXPRESS;Database=LearnORM;Trusted_Connection=True;";
            var schemaName = "dbo";
            var tableName = "Book";

            var columnsProvider = new ColumnsProvider(connectionString);
            var columns = columnsProvider.GetColumnsAsync(schemaName, tableName).GetAwaiter().GetResult();

            new Table(tableName, columns).OutputMigrationCode(Out);
        }
    }
}
