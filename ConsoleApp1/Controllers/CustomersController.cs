using ConsoleApp1.Exceptions;
using ConsoleApp1.Services;

namespace ConsoleApp1.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IDbService _dbService;

    public CustomersController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{id}/purchases")]
    public async Task<IActionResult> GetCustomer(int id)
    {
        try
        {
            var customer = await _dbService.GetCustomer(id);
            return Ok(customer);
        }
        catch(NotFoundException e)
        {
            return NotFound();
        }
    }
}