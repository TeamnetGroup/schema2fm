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
            var version = DateTime.Now.ToString("yyyyMMddHHmmss");

            writer.WriteLine( "using FluentMigrator;");
            writer.WriteLine();
            writer.WriteLine( "namespace Migrations");
            writer.WriteLine( "{");
            writer.WriteLine($"    [Migration({version})]");
            writer.WriteLine($"    public class {tableName}Migration : Migration");
            writer.WriteLine( "    {");
            writer.WriteLine( "        public override void Up()");
            writer.WriteLine( "        {");
            writer.Write    ($"            Create.Table(\"{tableName}\")");

            OutputColumnDefinitions(writer);

            writer.WriteLine( "        }");
            writer.WriteLine();
            writer.WriteLine( "        public override void Down()");
            writer.WriteLine( "        {");
            writer.WriteLine($"            Delete.Table(\"{tableName}\");");
            writer.WriteLine( "        }");
            writer.WriteLine( "    }");
            writer.WriteLine( "}");
        }

        private void OutputColumnDefinitions(TextWriter writer)
        {
            columns.ToList().ForEach(c =>
            {
                writer.WriteLine();
                writer.Write(c.FluentMigratorCode());
            });

            writer.WriteLine(@";");
        }
    }
}