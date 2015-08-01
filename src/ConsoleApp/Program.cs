using System;
using System.Linq;
using static System.Console;

namespace ConsoleApp
{
    class Program
    {
        private static void Main()
        {
            var columnsProvider = new ColumnsProvider(@"Server=.\SQLEXPRESS;Database=LearnORM;Trusted_Connection=True;");
            var tableName = "Book";
            var columns = columnsProvider.GetColumnsAsync("dbo", tableName).GetAwaiter().GetResult();

            var writer = Out;

            writer.Write(@"namespace Cucu
{
    [Migration(");
            writer.Write(DateTime.Now.ToString("yyyyMMddHHmmss"));
            writer.Write(@")]
    public class Vaca : Migration
    {
        public override void Up()
        {
            Create.Table(""");
            writer.Write(tableName);
            writer.Write(@""")");

            columns.ToList().ForEach(c =>
            {
                writer.WriteLine();
                writer.Write(c.FluentMigratorCode());
            });

            writer.WriteLine(@";
        }

        public override void Down()
        {
            // nothing here yet
        }
    }
}");
        }
    }
}
