// See https://aka.ms/new-console-template for more information
using Dapper;
using Dappers.Models;
using Microsoft.Data.SqlClient;


const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=Dapper;Trusted_Connection=True;MultipleActiveResultSets=True;";



using (var connection = new SqlConnection(connectionString))
{
    Console.WriteLine("Conectado");
    ListCategories(connection);
    //CreateCategory(connection);
    UpdateCategory(connection);



}

static void ListCategories(SqlConnection connection)
{
    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
    foreach (var item in categories)
    {
        Console.WriteLine($"{item.Id} - {item.Title}");
    }
}

static void CreateCategory(SqlConnection connection)
{
    var category = new Category();
    category.Id = Guid.NewGuid();
    category.Title = "Amazon AWS";
    category.Url = "amazon";
    category.Description = "Categoria p/ serviço AWS";
    category.Order = 8;
    category.Summary = "AWS Cloud";
    category.Featured = false;


    var insertSql = @"INSERT INTO
[Category] 
VALUES(
@Id, 
@Title, 
@Url, 
@Summary, 
@Order, 
@Description, 
@Featured)";

    var rows = connection.Execute(insertSql, new
    {
        category.Id,
        category.Title,
        category.Url,
        category.Summary,
        category.Order,
        category.Description,
        category.Featured
    });
    Console.WriteLine($"{rows} linhas inseridas");
}


//Criando o update
static void UpdateCategory(SqlConnection connection)
{
    var updateQuery = "UPDATE [Category] SET [Title]=@title WHERE [Id]=@id";
    var rows = connection.Execute(updateQuery, new
    {
        id = new Guid("ea8059a2-e679-4e74-99b5-e4f0b310fe6f"),
        title = "Frontend 2021"
    });

    Console.WriteLine($"{rows} Regristros atualizados!");
}



