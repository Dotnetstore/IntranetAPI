using Domain.Entities.System;
using Domain.EntityTypeConfigurations.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntityTypeConfigurations.System;

public class OwnCompanyEntityTypeConfiguration : CompanyBaseEntityTypeConfiguration<OwnCompany>
{
    public override void Configure(EntityTypeBuilder<OwnCompany> builder)
    {
        base.Configure(builder);

        builder
            .HasKey(q => q.Id);

        builder
            .Property(q => q.Id)
            .ValueGeneratedNever()
            .HasConversion(
                q => q.Id,
                q => new OwnCompanyId(q));
    }
}