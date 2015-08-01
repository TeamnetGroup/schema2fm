﻿using System.Linq;
using static System.Console;

namespace ConsoleApp
{
    class Program
    {
        private static void Main()
        {
            var columnsProvider = new ColumnsProvider(@"Server=.\SQLEXPRESS;Database=LearnORM;Trusted_Connection=True;");
            var columns = columnsProvider.GetColumnsAsync("dbo", "Book").GetAwaiter().GetResult();
            columns.ToList().ForEach(WriteLine);
        }
    }
}
