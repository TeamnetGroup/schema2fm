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

            using (DbConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
            }
        }
    }
}
