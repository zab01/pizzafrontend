using frontend.Models;
using frontend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace frontend.Controllers;

[ApiController]
[Route("api/items")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemsController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _itemService.GetItemAsync(id);
        return Ok(item);
    }
}