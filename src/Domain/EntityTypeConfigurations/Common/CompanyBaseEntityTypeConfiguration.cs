using Domain.Common;
using Domain.ValueObjects.CorporateIds;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.EntityTypeConfigurations.Common;

public abstract class CompanyBaseEntityTypeConfiguration<T> : BaseAuditableEntityEntityTypeConfiguration<T> where T : CompanyBase
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        string? value = null;
        
        var converter = new ValueConverter<CorporateId?, string?>(
            q => q.HasValue ? q.Value.Id : value,
            q => CorporateId.Create(q).Value);

        builder
            .Property(q => q.Name)
            .HasMaxLength(100)
            .IsRequired()
            .IsUnicode();

        builder
            .Property(q => q.CorporateId)
            .HasMaxLength(50)
            .IsRequired(false)
            .IsUnicode(false)
            .HasConversion(converter);
    }
}