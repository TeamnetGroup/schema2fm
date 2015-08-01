using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        private static void Main()
        {
            Work().GetAwaiter().GetResult();
        }

        private static async Task Work()
        {
            const string connectionString = @"Server=.\SQLEXPRESS;Database=LearnORM;Trusted_Connection=True;";
            const string schemaName = "dbo";
            const string tableName = "Book";

            using (DbConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"select t.object_id from sys.tables t
inner join sys.schemas s
on t.schema_id = s.schema_id
where s.name = @schemaName and t.name = @tableName";
                    var schemaNameParameter = command.CreateParameter();
                    schemaNameParameter.DbType = DbType.String;
                    schemaNameParameter.ParameterName = "@schemaName";
                    schemaNameParameter.Value = schemaName;
                    command.Parameters.Add(schemaNameParameter);

                    var tableNameParameter = command.CreateParameter();
                    tableNameParameter.DbType = DbType.String;
                    tableNameParameter.ParameterName = "@tableName";
                    tableNameParameter.Value = tableName;
                    command.Parameters.Add(tableNameParameter);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var tableId = reader.GetFieldValue<int>(0);
                            Console.WriteLine($"Table has object id {tableId}");
                        }
                    }
                }
            }
        }
    }
}
