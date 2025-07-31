using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketManagement.Domain.Tickets;

namespace TicketManagement.Infrastructure.Domain.Tickets;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");
        builder.HasKey(x => x.Id);
        builder.Property(t => t.Id)
            .HasConversion(
                ticketId => ticketId.Value,           // Convert TicketId to Guid for database
                guid => new TicketId(guid))           // Convert Guid from database to TicketId
            .IsRequired()
            .HasColumnName("Id");

        builder.Property(t => t.CreationDateTime)
            .IsRequired()
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(t => t.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20)
            .IsUnicode(false);

        builder.Property(t => t.Governorate)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.District)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Status)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(TicketStatus.Open);

        builder.Property(t => t.HandledDateTime)
            .HasColumnType("datetime2")
            .IsRequired(false);

        // Indexes for better query performance
        builder.HasIndex(t => t.CreationDateTime)
            .HasDatabaseName("IX_Tickets_CreationDateTime");

        builder.HasIndex(t => t.Status)
            .HasDatabaseName("IX_Tickets_Status");

        builder.HasIndex(t => new { t.Status, t.CreationDateTime })
            .HasDatabaseName("IX_Tickets_Status_CreationDateTime");

        // Additional constraints
        builder.HasCheckConstraint("CK_Tickets_PhoneNumber", 
            "LEN([PhoneNumber]) >= 10");
        
        builder.HasCheckConstraint("CK_Tickets_Status_Valid",
            "[Status] IN ('Open', 'Handled')");

       
    }
}