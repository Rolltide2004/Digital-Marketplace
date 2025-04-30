using Api.COP4870.Controllers.EC;
using COP4870.DTO;
using COP4870.Models;
using COP4870.Util;
using Microsoft.AspNetCore.Mvc;

namespace Api.COP4870.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly ILogger<CartController> _logger;

    public CartController(ILogger<CartController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Item?> Get()
    {
        return new CartEC().Get();
    }

    [HttpGet("{id}")]
    public Item? GetById(int id)
    {
        return new CartEC().Get().FirstOrDefault(i => i?.Id == id);
    }

    [HttpDelete("{id}")]
    public Item? Delete(int id)
    {
        return new CartEC().Delete(id);
    }

    [HttpPost]
    public Item? AddOrUpdate([FromBody] Item item)
    {

        var newItem = new CartEC().AddOrUpdate(item);
        return item;
    }

    [HttpPost("Search")]
    public IEnumerable<Item> Search([FromBody] QueryRequest query)
    {
        return new CartEC().Get(query.Query);
    }
}
