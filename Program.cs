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
    //UpdateCategory(connection);
    //GetCategory(connection);
    //DeleteCategory(connection);
    CreateManyCategory(connection);

}

static void ListCategories(SqlConnection connection)
{
    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
    foreach (var item in categories)
    {
        Console.WriteLine($"{item.Id} - {item.Title}");
    }
}

static void GetCategory(SqlConnection connection)
{
    var category = connection
        .QueryFirstOrDefault<Category>(
            "SELECT TOP 1 [Id], [Title] FROM [Category] WHERE [Id]=@id",
            new
            {
                id = "af3407aa-11ae-4621-a2ef-2028b85507c4"
            });
    Console.WriteLine($"{category.Id} - {category.Title}");
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

static void DeleteCategory(SqlConnection connection)
{
    var deleteQuery = "DELETE [Category] WHERE [Id]=@id";
    var rows = connection.Execute(deleteQuery, new
    {
        id = new Guid("ea8059a2-e679-4e74-99b5-e4f0b310fe6f"),
    });
    Console.WriteLine($"{rows} Registros excluidos");
}

static void CreateManyCategory(SqlConnection connection)
{
    var category = new Category();
    category.Id = Guid.NewGuid();
    category.Title = "Amazon AWS";
    category.Url = "amazon";
    category.Description = "Categoria destinada a serviços do AWS";
    category.Order = 8;
    category.Summary = "AWS Cloud";
    category.Featured = false;

    var category2 = new Category();
    category2.Id = Guid.NewGuid();
    category2.Title = "Categoria Nova";
    category2.Url = "categoria-nova";
    category2.Description = "Categoria nova";
    category2.Order = 9;
    category2.Summary = "Categoria";
    category2.Featured = true;

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

    var rows = connection.Execute(insertSql, new[]{
                new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                },
                new
                {
                    category2.Id,
                    category2.Title,
                    category2.Url,
                    category2.Summary,
                    category2.Order,
                    category2.Description,
                    category2.Featured
                }
            });

    Console.WriteLine($"{rows} linhas inseridas");
}