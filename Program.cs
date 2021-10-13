// See https://aka.ms/new-console-template for more information
using Dapper;
using Dappers.Models;
using Microsoft.Data.SqlClient;


const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=Dapper;Trusted_Connection=True;MultipleActiveResultSets=True;";

using (var connection = new SqlConnection(connectionString))
{
    Console.WriteLine("Conectado");

    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
    foreach (var category in categories)
    {
        Console.WriteLine($"{category.Id} - {category.Title}");
    }
    //connection.Open();

    //using (var command = new SqlCommand())
    //{
    //    command.Connection = connection;
    //    command.CommandType = System.Data.CommandType.Text;
    //    command.CommandText = "SELECT [Id], [Title] FROM [Category]";

    //    var reader = command.ExecuteReader();
    //    while (reader.Read())
    //    {
    //        Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
    //    }
    //}
}



