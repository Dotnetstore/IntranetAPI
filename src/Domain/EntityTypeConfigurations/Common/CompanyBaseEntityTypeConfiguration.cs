using Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntityTypeConfigurations.Common;

public abstract class CompanyBaseEntityTypeConfiguration<T> : BaseAuditableEntityEntityTypeConfiguration<T> where T : CompanyBase
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        builder
            .Property(q => q.Name)
            .HasMaxLength(100)
            .IsRequired()
            .IsUnicode();

        builder
            .Property(q => q.CorporateId)
            .HasMaxLength(50)
            .IsRequired(false)
            .IsUnicode(false);
    }
}