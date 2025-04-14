using Api.COP4870.Controllers.EC;
using COP4870.DTO;
using COP4870.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.COP4870.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{
    private readonly ILogger<InventoryController> _logger;

    public InventoryController(ILogger<InventoryController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Item?> Get()
    {
        return new InventoryEC().Get();
    }

    [HttpGet("{id}")]
    public Item? GetById(int id)
    {
        return new InventoryEC().Get().FirstOrDefault(i => i?.Id == id);
    }

    [HttpDelete("{id}")]
    public Item? Delete(int id)
    {
        return new InventoryEC().Delete(id);
    }
    [HttpPost]
    public Item? AddOrUpdate([FromBody]Item item) {
        
        var newItem = new InventoryEC().AddOrUpdate(item);
        return item;
    }
}
