using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ColumnsProvider
    {
        public async Task<IEnumerable<string>> GetColumnsAsync(string schemaName, string tableName)
        {
            const string connectionString = @"Server=.\SQLEXPRESS;Database=LearnORM;Trusted_Connection=True;";

            using (DbConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"select c.name, c.is_nullable, c.max_length, types.name [type] from sys.columns c
inner join sys.types types on c.system_type_id = types.user_type_id
inner join sys.tables t
on t.object_id = c.object_id
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
                        var columns = new List<string>();
                        while (await reader.ReadAsync())
                        {
                            var columnName = reader.GetFieldValue<string>(0);
                            var isNullable = reader.GetFieldValue<bool>(1);
                            var maxLength = reader.GetFieldValue<short>(2);
                            var type = reader.GetFieldValue<string>(3);
                            columns.Add($"Column {columnName} is {type}({maxLength}) and {isNullable}");
                        }

                        return columns;
                    }
                }
            }
        }
    }
}