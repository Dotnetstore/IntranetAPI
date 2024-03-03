using Application.Common.Interfaces;
using Domain.Entities.System;
using FluentAssertions;
using Infrastructure.Contexts;
using Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Persistence.Common;

public class GenericRepositoryTests : IDisposable
{
    private bool _disposed;
    private readonly ApplicationDataContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CancellationToken _cancellationToken;

    public GenericRepositoryTests()
    {
        var options = new DbContextOptionsBuilder()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _cancellationToken = new CancellationTokenSource().Token;
        _context = new ApplicationDataContext(options);
        _unitOfWork = new UnitOfWork(_context);
    }

    [Fact]
    public void Create_should_set_entity_state_to_created()
    {
        var ownCompany = new OwnCompany
        {
            CreatedDate = DateTimeOffset.Now,
            Id = new OwnCompanyId(Guid.NewGuid()),
            Name = "Name"
        };
        
        _unitOfWork.Repository<OwnCompany>().Create(ownCompany);

        _context.Entry(ownCompany).State.Should().Be(EntityState.Added);
    }

    [Fact]
    public void Update_should_set_entity_state_to_modified()
    {
        var ownCompany = new OwnCompany
        {
            CreatedDate = DateTimeOffset.Now,
            Id = new OwnCompanyId(Guid.NewGuid()),
            Name = "Name"
        };
        
        _unitOfWork.Repository<OwnCompany>().Update(ownCompany);

        _context.Entry(ownCompany).State.Should().Be(EntityState.Modified);
    }

    [Fact]
    public void Delete_should_set_entity_state_to_deleted()
    {
        var ownCompany = new OwnCompany
        {
            CreatedDate = DateTimeOffset.Now,
            Id = new OwnCompanyId(Guid.NewGuid()),
            Name = "Name"
        };
        
        _unitOfWork.Repository<OwnCompany>().Delete(ownCompany);

        _context.Entry(ownCompany).State.Should().Be(EntityState.Deleted);
    }

    [Fact]
    public async Task GetAll_should_return_empty_list()
    {
        var result = await _unitOfWork
            .Repository<OwnCompany>()
            .GetAllAsync(_cancellationToken);

        result.Should().HaveCount(0);
    }

    [Fact]
    public async Task Get_should_return_empty_list()
    {
        var result = await _unitOfWork
            .Repository<OwnCompany>()
            .Entities
            .ToListAsync(_cancellationToken);

        result.Should().HaveCount(0);
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _context.Dispose();
        }

        _disposed = true;
    }
}