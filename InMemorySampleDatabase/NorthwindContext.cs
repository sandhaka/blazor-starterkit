using Microsoft.EntityFrameworkCore;
using InMemorySampleDatabase.Entities;

namespace InMemorySampleDatabase;

/// <summary>
/// The Northwind context.
/// </summary>
public partial class NorthwindContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CustomerDemographic>()
            .HasKey(e => new { e.CustomerID, e.DemographicID });
        modelBuilder.Entity<EmployeeTerritory>()
            .HasKey(e => new { e.EmployeeID, e.TerritoryID });
        modelBuilder.Entity<OrderDetail>()
            .HasKey(e => new { e.OrderID, e.ProductID });
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NorthwindContext"/> class.
    /// </summary>
    public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the categories.
    /// </summary>
    public DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Gets or sets the customer demographics.
    /// </summary>
    public DbSet<CustomerDemographic> CustomerDemographics { get; set; }

    /// <summary>
    /// Gets or sets the customers.
    /// </summary>
    public DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// Gets or sets the demographics.
    /// </summary>
    public DbSet<Demographic> Demographics { get; set; }

    /// <summary>
    /// Gets or sets the employees.
    /// </summary>
    public DbSet<Employee> Employees { get; set; }

    /// <summary>
    /// Gets or sets the employee territories.
    /// </summary>
    public DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }

    /// <summary>
    /// Gets or sets the order details.
    /// </summary>
    public DbSet<OrderDetail> OrderDetails { get; set; }

    /// <summary>
    /// Gets or sets the orders.
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// Gets or sets the products.
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <summary>
    /// Gets or sets the regions.
    /// </summary>
    public DbSet<Region> Regions { get; set; }

    /// <summary>
    /// Gets or sets the shippers.
    /// </summary>
    public DbSet<Shipper> Shippers { get; set; }

    /// <summary>
    /// Gets or sets the suppliers.
    /// </summary>
    public DbSet<Supplier> Suppliers { get; set; }

    /// <summary>
    /// Gets or sets the territories.
    /// </summary>
    public DbSet<Territory> Territories { get; set; }
}