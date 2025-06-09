using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<WProgram> Programs { get; set; }
    public DbSet<WashingMachine> WashingMachines { get; set; }
    public DbSet<AvailableProgram> AvailablePrograms { get; set; }
    public DbSet<PurchaseHistory> PurchaseHistories { get; set; }

    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasData(new List<Customer>()
        {
            new Customer(){ CustomerId = 1, FirstName = "Max", LastName = "Burger", PhoneNumber = "732241222"},
            new Customer(){ CustomerId = 2, FirstName = "John", LastName = "Doe", PhoneNumber = "111111111"},
            new Customer(){ CustomerId = 3, FirstName = "Jane", LastName = "Doe", PhoneNumber = "222222222"},
            new Customer() {CustomerId = 4, FirstName = "John", LastName = "Weak", PhoneNumber = null}
        });

        modelBuilder.Entity<WProgram>().HasData(new List<WProgram>()
        {
            new WProgram(){ProgramId = 1, Name = "Program1", DurationMinutes = 10, TemperatureCelsius = 30},
            new WProgram(){ProgramId = 2, Name = "Program2", DurationMinutes = 20, TemperatureCelsius = 50},
            new WProgram(){ProgramId = 3, Name = "Program3", DurationMinutes = 30, TemperatureCelsius = 20},
        });

        modelBuilder.Entity<WashingMachine>().HasData(new List<WashingMachine>()
        {
            new WashingMachine(){WashingMachineId = 1, MaxWeight = 200, SerialNumber = "222"},
            new WashingMachine(){WashingMachineId = 2, MaxWeight = 300, SerialNumber = "333"},
            new WashingMachine(){WashingMachineId = 3, MaxWeight = 500, SerialNumber = "528"}
        });

        modelBuilder.Entity<PurchaseHistory>().HasData(new List<PurchaseHistory>()
        {
            new PurchaseHistory(){AvailableProgramId = 1, CustomerId = 1, PurchaseDate = DateTime.Parse("2025-02-02"), Rating = 5},
            new PurchaseHistory(){AvailableProgramId = 2, CustomerId = 2, PurchaseDate = DateTime.Parse("2024-12-12"), Rating = null}
        });

        modelBuilder.Entity<AvailableProgram>().HasData(new List<AvailableProgram>()
        {
            new AvailableProgram(){AvailableProgramId = 1, WashingMachineId = 1, ProgramId = 2, Price = 22.55},
            new AvailableProgram(){AvailableProgramId = 2, WashingMachineId = 2, ProgramId = 1, Price = 22.99}
        });
    }
}