using System.ComponentModel.DataAnnotations;
using ConsoleApp1.Data;
using ConsoleApp1.DTOs;
using ConsoleApp1.Exceptions;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<CustomerDto> GetCustomer(int id)
    {
        var customer = await _context.Customers
            .Where(c => c.CustomerId == id)
            .Select(c => new CustomerDto()
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                Purchases = c.PurchaseHistories.Select(p => new PurchaseDto()
                {
                    Date = p.PurchaseDate,
                    Rating = p.Rating,
                    Price = p.AvailableProgram.Price,
                    WashingMachine = new WashingMachineDto()
                    {
                        Serial = p.AvailableProgram.WashingMachine.SerialNumber,
                        MaxWeight = p.AvailableProgram.WashingMachine.MaxWeight
                    },
                    Program = new ProgramDto()
                    {
                        Name = p.AvailableProgram.Program.Name,
                        Duration = p.AvailableProgram.Program.DurationMinutes
                    }
                    
                }).ToList()
            })
            .FirstOrDefaultAsync();
        
        if (customer == null)
            throw new NotFoundException();
        
        return customer;
    }

    public async Task AddWashingMachineWithPrograms(WashingMachineDto washingMachineDto, List<AvailableProgramDto> availableprograms)
    {
        if (washingMachineDto.MaxWeight < 8)
        {
            throw new ValidationException("Max weight cannot be less than 8");
        }

        if (await _context.WashingMachines.AnyAsync(wm => wm.SerialNumber == washingMachineDto.Serial))
        {
            throw new ConflictException("WashingMachine with this serial number already exists");
        }

        var washingMachine = new WashingMachine()
        {
            MaxWeight = washingMachineDto.MaxWeight,
            SerialNumber = washingMachineDto.Serial
        };

        var availablePrograms = new List<AvailableProgram>();
        foreach (var availableProgramDto in availablePrograms)
        {
            if (availableProgramDto.Price < 25)
            {
                throw new ValidationException("The price of a program cannot exceed 25");
            }

            var program = await _context.Programs
                .FirstOrDefaultAsync(p => p.Name == availableProgramDto.Program.Name);
            if (program == null)
            {
                throw new NotFoundException();
            }
            
            availablePrograms.Add(new AvailableProgram()
            {
                ProgramId = program.ProgramId,
                Price = availableProgramDto.Price,
                Program = program,
                WashingMachine = washingMachine
            });
        }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                await _context.WashingMachines.AddAsync(washingMachine);
                await _context.AvailablePrograms.AddRangeAsync(availablePrograms);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}