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
            const string format = "yyyyMMddHHmmss";
            writer.WriteLine(@"namespace Migrations
{");
            writer.WriteLine($"    [Migration(\"{DateTime.Now.ToString(format)}\")]");
            writer.Write($"    public class {tableName}Migration : Migration");
            writer.WriteLine(@"
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