using Presentation.Extensions;
using Presentation.Factories;

var builder = WebApplication.CreateBuilder(args);

var logger = builder.CreateLogger();

await builder
    .AddServices(logger)
    .AddLogger(logger)
    .BuildApplication()
    .LoadDb()
    .CheckIfDatabaseIsUpdated()
    .AddSwagger()
    .AddMiddleware()
    .AddApplicationServices()
    .RunAppAsync();
    
namespace Presentation
{
    public partial class Program
    {
    }
}