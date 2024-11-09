using frontend.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Data.SqlClient;

namespace frontend.Services;

public class PizzaService : IPizzaService
{
    public IEnumerable<PizzaInfo> GetPizzas()
    {
        var pizzas = new List<PizzaInfo>
        {
            new PizzaInfo { PizzaNome = "Almondegas recheadas", Ingredientes = "Almondegas,muzzarela e tomate", PizzaPreco = 45.90M, EmEstoque = "sim"},
            new PizzaInfo { PizzaNome = "Frutos do Mar", Ingredientes = "Frutos do mar com recheio e tomate", PizzaPreco = 75.50M, EmEstoque = "não"},
            new PizzaInfo { PizzaNome = "Cogumelos com rúcula ", Ingredientes = "Cogumelos, rúcula e tomate seco", PizzaPreco = 32.45M, EmEstoque = "sim"},
            new PizzaInfo { PizzaNome = "Serrana", Ingredientes = "Acabaxi, bacon, pimenta, mussarela", PizzaPreco = 45.50M, EmEstoque = "sim"},
            new PizzaInfo { PizzaNome = "Calabreza da Casa", Ingredientes = "Calabreza, mussarela, tomate", PizzaPreco = 35.99M, EmEstoque = "não"},
            new PizzaInfo { PizzaNome = "Muzzarela Especial", Ingredientes = "Mussarela, tomate, orégano, queijo", PizzaPreco = 40.75M, EmEstoque = "sim"},
            new PizzaInfo { PizzaNome = "Portuguesa Especial", Ingredientes = "Mussarela, Presunto, ovos, ervila e palmito", PizzaPreco = 51.50M, EmEstoque = "sim"}
        };
        return pizzas;
    }

    public IEnumerable<PizzaInfo> GetPizzasDB_SQlite()
    {
        List<PizzaInfo> pizzas;
        PizzaInfo pizza;

        using (var connection = new SqliteConnection("Data Source=cardapio.db"))
        {
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
            @"
                SELECT * FROM Pizzas
            ";

            using (var reader = command.ExecuteReader())
            {
                pizzas = new List<PizzaInfo>();

                while (reader.Read())
                {
                    pizza = new PizzaInfo() {PizzaNome = reader.GetString(0), Ingredientes = reader.GetString(1), PizzaPreco = reader.GetDecimal(2), EmEstoque = reader.GetString(3)};
                
                    pizzas.Add(pizza);
                }
            }
        }
        return pizzas;
    }

    public IEnumerable<PizzaInfo> GetPizzasDB_SQLServer()
    {
        List<PizzaInfo> pizzas;
        PizzaInfo pizza;

        //using (var connection = new SqlConnection("Server=10.109.180.107,1433;Initial Catalog=Cardapio;User ID=sa;Password=numsey#2021;TrustServerCertificate=True"))
        using (var connection = new SqlConnection("Server=mssql-server,1433;Initial Catalog=Cardapio;User ID=sa;Password=numsey#2021;TrustServerCertificate=True"))
        {
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
            @"
                SELECT * FROM Pizzas
            ";

            using (var reader = command.ExecuteReader())
            {
                pizzas = new List<PizzaInfo>();

                while (reader.Read())
                {
                    pizza = new PizzaInfo() {PizzaNome = reader.GetString(0), Ingredientes = reader.GetString(1), PizzaPreco = (decimal)reader.GetDouble(2), EmEstoque = reader.GetString(3)};
                
                    pizzas.Add(pizza);
                }
            }
        }
        return pizzas;
    }
}