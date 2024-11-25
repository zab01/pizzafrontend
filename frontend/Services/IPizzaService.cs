using frontend.Models;

namespace frontend.Services;

public interface IPizzaService
{
   IEnumerable<PizzaInfo> GetPizzas();
   IEnumerable<PizzaInfo> GetPizzasDB_SQlite(string conStr);
   IEnumerable<PizzaInfo> GetPizzasDB_SQLServer(string conStr);
}
