using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    public class Table
    {
        private readonly string tableName;
        private readonly IEnumerable<Column> columns;

        public Table(string tableName, IEnumerable<Column> columns)
        {
            this.tableName = tableName;
            this.columns = columns;
        }

        public void OutputMigrationCode(TextWriter writer)
        {
            writer.Write(@"namespace Cucu
{
    [Migration(");
            writer.Write(DateTime.Now.ToString("yyyyMMddHHmmss"));
            writer.WriteLine(@")]
    public class Vaca : Migration
    {
        public override void Up()
        {");
            writer.Write($"            Create.Table(\"{tableName}\")");

            columns.ToList().ForEach(c =>
            {
                writer.WriteLine();
                writer.Write(c.FluentMigratorCode());
            });

            writer.WriteLine(@";
        }

        public override void Down()
        {");
            writer.Write($"            Delete.Table(\"{tableName}\");");
            writer.WriteLine(@"
        }
    }
}");
        }
    }
}