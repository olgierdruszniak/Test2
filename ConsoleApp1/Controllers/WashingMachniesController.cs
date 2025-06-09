using System.ComponentModel.DataAnnotations;
using ConsoleApp1.DTOs;
using ConsoleApp1.Exceptions;
using ConsoleApp1.Models;
using ConsoleApp1.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleApp1.Controllers;

[ApiController]
[Route("api")]
public class WashingMachinesController : ControllerBase
{
    private readonly IDbService _dbService;

    public WashingMachinesController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpPost("washing-machines")]
    public async Task<IActionResult> AddWashingMashine([FromBody] AddWashingMachineRequest request)
    {
        try
        {
            await _dbService.AddWashingMachineWithPrograms(request.WashingMachineDto, request.AvailablePrograms);
            return Created(nameof(AddWashingMashine), null);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }
}