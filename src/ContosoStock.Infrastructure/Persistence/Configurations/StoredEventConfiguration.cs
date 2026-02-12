using ContosoStock.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoStock.Infrastructure.Persistence.Configurations;

public class StoredEventConfiguration : IEntityTypeConfiguration<StoredEvent>
{
    public void Configure(EntityTypeBuilder<StoredEvent> builder)
    {
        builder.ToTable("Events");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.AggregateId);
        
        builder.Property(x => x.MessageType)
            .IsRequired()
            .HasMaxLength(200);
        
        // Mapear como JSONB no Postgres
        builder.Property(x => x.Data)
            .HasColumnType("jsonb") 
            .IsRequired();
        
        builder.Property(x => x.Version)
            .IsRequired();
        
        builder.Property(x => x.OccurredOn)
            .IsRequired();
    }
}