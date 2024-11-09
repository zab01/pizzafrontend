using frontend.Models;
using frontend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace frontend.Controllers;

public class HomeController : Controller
{
    private static string? ErrorMessage;
    private readonly IPizzaService _service;
    public IEnumerable<PizzaInfo> Pizzas;
    public HomeController(IPizzaService service)
    {
        _service = service;
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult GetPizzas()
    {
        try {
            Pizzas = _service.GetPizzasDB_SQLServer();
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