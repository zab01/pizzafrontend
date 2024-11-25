using frontend.Models;
using frontend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace frontend.Controllers;

public class HomeController : Controller
{   private readonly IPizzaService _service;
    private readonly IConfiguration _configuration;
    private static string? ErrorMessage;
    public IEnumerable<PizzaInfo> Pizzas;
    public HomeController(IPizzaService service, IConfiguration configuration)
    {
        _service = service;
        _configuration = configuration;
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult GetPizzas()
    {
        var dbServer = _configuration.GetValue<string>("DbServer");
        var conStr = _configuration.GetValue<string>("ConStr");
        
        try {
            if (dbServer == "SQlite")
                Pizzas = _service.GetPizzasDB_SQlite(conStr);
            else
                Pizzas = _service.GetPizzasDB_SQLServer(conStr);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            Console.WriteLine("[error]:" + ErrorMessage);
        }

        if (Pizzas is null)
        {
            //return View("Error");
            return RedirectToAction("Error");
        }

        return View(Pizzas);
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { ErrorMessage = ErrorMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}