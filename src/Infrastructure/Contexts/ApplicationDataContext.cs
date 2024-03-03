using Domain;
using Domain.Entities.System;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class ApplicationDataContext : DbContext
{
    public ApplicationDataContext(
        DbContextOptions options) : base(options)
    {
    }

    public DbSet<OwnCompany> OwnCompanies => Set<OwnCompany>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IDomainAssemblyMarker).Assembly);
    }
}