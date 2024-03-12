using Bogus;
using Bogus.Extensions.Sweden;
using Domain.Entities.System;
using Domain.ValueObjects.CorporateIds;

namespace TestHelper.FakeData;

public static class SystemFakeData
{
    public static IEnumerable<OwnCompany> GenerateOwnCompanyFakeData(int quantity)
    {
        var faker = new Faker<OwnCompany>("sv")
            .RuleFor(q => q.Id, f => new OwnCompanyId(f.Random.Guid()))
            .RuleFor(q => q.Name, f => f.Company.CompanyName())
            .RuleFor(q => q.CorporateId, f => CorporateId.Create(f.Person.Personnummer().OrNull(f, .8f)).Value);

        return faker.Generate(quantity).ToList();
    }
}