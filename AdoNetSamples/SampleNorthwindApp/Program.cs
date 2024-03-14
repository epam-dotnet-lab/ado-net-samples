using System.Data.Common;
using Microsoft.Data.SqlClient;
using static System.Console;

namespace SampleNorthwindApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            ReadFromMsSql(connectionString);
            ReadFromDatabase(new SqlConnection(connectionString));
        }

        private static void ReadFromMsSql(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string productName = "Chai";
            string query = $"SELECT ProductID, ProductName FROM Products WHERE ProductName = @productName;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@productName", productName);

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                int id = dataReader.GetInt32(0);
                string name = dataReader.GetString(1);

                WriteLine($"{id}, {name}");
            }
        }

        private static void ReadFromDatabase(DbConnection connection /*, string connectionString*/)
        {
            connection.Open();

            string productName = "Chai";
            string query = $"SELECT ProductID, ProductName FROM Products WHERE ProductName = @productName;";
            DbCommand command = connection.CreateCommand();
            command.CommandText = query;

            DbParameter productNameParameter = command.CreateParameter();
            productNameParameter.ParameterName = "@productName";
            productNameParameter.Value = productName;

            command.Parameters.Add(productNameParameter);

            DbDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                int id = dataReader.GetInt32(0);
                string name = dataReader.GetString(1);

                WriteLine($"{id}, {name}");
            }
        }
    }
}
