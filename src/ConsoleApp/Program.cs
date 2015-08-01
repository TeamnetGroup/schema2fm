﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

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
