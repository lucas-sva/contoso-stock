using ContosoStock.Domain.Fulfillment.Models;
using ContosoStock.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoStock.Infrastructure.Persistence.Contexts;

public class ContosoStockDbContext(DbContextOptions<ContosoStockDbContext> options) : DbContext(options)
{
    public DbSet<StockLot> StockLots { get; set; }
    public DbSet<StoredEvent> Events { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContosoStockDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}