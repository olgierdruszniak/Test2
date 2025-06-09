using ConsoleApp1.DTOs;
using ConsoleApp1.Models;

namespace ConsoleApp1.Services;

public interface IDbService
{
    Task<CustomerDto> GetCustomer(int id);
    
    Task AddWashingMachineWithPrograms(WashingMachineDto washingMachineDto, List<AvailableProgramDto> availableprograms);
}