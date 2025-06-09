namespace ConsoleApp1.DTOs;

public class CustomerDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public List<PurchaseDto> Purchases { get; set; } = null!;
}

public class PurchaseDto
{
    public DateTime Date { get; set; }
    public int? Rating { get; set; }
    public double Price { get; set; }
    public WashingMachineDto WashingMachine { get; set; } = null!;
    public ProgramDto Program { get; set; } = null!;
}

public class WashingMachineDto
{
    public string Serial { get; set; } = null!;
    public double MaxWeight { get; set; }
}

public class ProgramDto
{
    public string Name { get; set; } = null!;
    public int Duration { get; set; }
}