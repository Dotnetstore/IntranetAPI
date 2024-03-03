using Domain.Common;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Domain.Tests.Common;

public class BaseAuditableEntityTests
{
    [Fact]
    public void Should_contain_correct_properties()
    {
        var item = new TestBaseAuditableEntityClass
        {
            ConcurrencyToken = Guid.NewGuid().ToByteArray(),
            CreatedBy = Guid.NewGuid(),
            CreatedDate = DateTimeOffset.Now,
            DeletedBy = Guid.NewGuid(),
            DeletedDate = DateTimeOffset.Now,
            IsDeleted = true,
            IsSystem = true,
            IsGdpr = true,
            LastUpdatedDate = DateTimeOffset.Now,
            LastUpdatedBy = Guid.NewGuid()
        };

        using (new AssertionScope())
        {
            item.ConcurrencyToken.Should().NotBeNull();
            item.CreatedBy.Should().NotBeEmpty();
            item.DeletedBy.Should().NotBeEmpty();
            item.LastUpdatedBy.Should().NotBeEmpty();
            item.CreatedDate.Should().BeAfter(DateTimeOffset.MinValue);
            item.DeletedDate.Should().BeAfter(DateTimeOffset.MinValue);
            item.LastUpdatedDate.Should().BeAfter(DateTimeOffset.MinValue);
            item.IsDeleted.Should().BeTrue();
            item.IsSystem.Should().BeTrue();
            item.IsGdpr.Should().BeTrue();
        }
    }
}

public class TestBaseAuditableEntityClass : BaseAuditableEntity
{}