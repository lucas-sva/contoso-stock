using ContosoStock.Domain.Fulfillment.Models;
using ContosoStock.Domain.Fulfillment.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoStock.Infrastructure.Persistence.Configurations;

public class StockLotConfiguration : IEntityTypeConfiguration<StockLot>
{
    public void Configure(EntityTypeBuilder<StockLot> builder)
    {
        builder.ToTable("StockLots");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Sku);
        
        builder.Property(x => x.Sku)
            .HasConversion(
                sku => sku!.Value,
                value => Sku.Create(value).Value)
            .IsRequired();
        
        builder.Property(x => x.Quantity)
            .IsRequired();
        
        builder.Property(x => x.ExpirationDate)
            .IsRequired();
        
        builder.Property(x => x.IsFragile)
            .IsRequired();
        
        // Mapeando o Version para a consistência do banco
        builder.Property(x => x.Version)
            .IsConcurrencyToken();
    }
}