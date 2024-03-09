using Application.Common.Interfaces;
using Infrastructure.Contexts;
using Infrastructure.Persistence.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Npgsql;
using Presentation;

namespace TestHelper;

public class DotnetstoreApiFactory : WebApplicationFactory<Program>
{
    public IUnitOfWork UnitOfWork { get; private set; } = null!;
    public ApplicationDataContext ApplicationDataContext { get; private set; } = null!;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });
    
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(ApplicationDataContext));
            services.AddDbContext<ApplicationDataContext>(q =>
            {
                q.UseNpgsql(BuildConnectionString(
                    "localhost",
                    "mydb",
                    "dotnetstore",
                    "dotnetstoretestpassword",
                    5432));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var provider = services.BuildServiceProvider();
            UnitOfWork = provider.GetRequiredService<IUnitOfWork>();
            ApplicationDataContext = provider.GetRequiredService<ApplicationDataContext>();
        });
    }
    
    private static string BuildConnectionString(
        string server,
        string database, 
        string username, 
        string password, 
        int port)
    {
        var connectionString = new NpgsqlConnectionStringBuilder
        {
            Host = server,
            Database = database,
            Username = username,
            Password = password,
            Port = port
        };
    
        return connectionString.ConnectionString;
    }
}