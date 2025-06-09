namespace ConsoleApp1.DTOs;

public class AddWashingMachineRequest
{
    public WashingMachineDto WashingMachineDto { get; set; }
    public List<AvailableProgramDto> AvailablePrograms { get; set; }
    
}



public class AvailableProgramDto
{
    public string Name { get; set; }
    public double Price { get; set; }
}