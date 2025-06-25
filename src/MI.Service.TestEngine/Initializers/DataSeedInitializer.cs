using Microsoft.EntityFrameworkCore;
using MI.Service.TestEngine.Domain.Entities;
using MI.Service.TestEngine.Infrastructure.Configuration;
using MI.Service.TestEngine.Infrastructure.Persistence;

namespace MI.Service.TestEngine.Initializers;

/// <inheritdoc/>
public class DataSeedInitializer : IDataSeedInitializer
{
    
    /// <summary>
    /// Initializes a new instance of the <see cref="DataSeedInitializer"/> class.
    /// </summary>
    public DataSeedInitializer()
    {        
    }

    /// <inheritdoc/>
    public async Task InitializeSeedData(IServiceProvider serviceProvider)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var systemConfiguration = configuration.GetSystemConfigurationModel().SystemEntityPath;
        var entitiesReader = serviceProvider.GetRequiredService<IEntityReader>();
        var initialEntities = await entitiesReader.Read(systemConfiguration);

        if (initialEntities is not null)
        {
            var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            await InitializeApplication(applicationDbContext, initialEntities.Applications);
            await InitializeModule(applicationDbContext, initialEntities.Modules);
        }
    }

    private static async Task InitializeApplication(ApplicationDbContext context, IReadOnlyCollection<Application> applications)
    {
        if (applications is null || !applications.Any())
        {
            return;
        }

        foreach (var item in applications)
        {
            var application = await context.Applications.FirstOrDefaultAsync(x => x.SystemName == item.SystemName);

            if (application == null)
            {
                await context.Applications.AddAsync(item);
            }
            else
            {
                application.Name = item.Name;
                application.IsActive = item.IsActive;
                context.Applications.Update(application);
            }
        }

        await context.SaveChangesAsync();
    }

    private static async Task InitializeModule(ApplicationDbContext context, IReadOnlyCollection<Module> modules)
    {
        if (modules is null || !modules.Any())
        {
            return;
        }

        foreach (var item in modules)
        {
            var module = await context.Modules.FirstOrDefaultAsync(x => x.SystemName == item.SystemName);

            if (module == null)
            {
                await context.Modules.AddAsync(item);
            }
            else
            {
                module.Name = item.Name;
                module.IsActive = item.IsActive;
                module.ApplicationSystemName = item.ApplicationSystemName;

                context.Modules.Update(module);
            }
        }

        await context.SaveChangesAsync();
    }

    /// <summary>Imports the seed data.</summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="accountId">The account identifier.</param>
    public async Task ImportSeedData(IServiceProvider serviceProvider, string accountId)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var systemConfiguration = configuration.GetSystemConfigurationModel().SystemEntityPath;
        var entitiesReader = serviceProvider.GetRequiredService<IEntityReader>();
        var initialEntities = await entitiesReader.Read(systemConfiguration);

        if (initialEntities is not null)
        {
            var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var canConnect = await applicationDbContext.Database.CanConnectAsync();
            if (canConnect)
            {
                await CreateApplications(applicationDbContext, initialEntities.Applications, accountId);

                await CreateModule(applicationDbContext, initialEntities.Modules, accountId);
            }            
        }
    }

    private static async Task CreateApplications(ApplicationDbContext context, IReadOnlyCollection<Application> applications, string accountId)
    {
        if (applications is null || !applications.Any())
        {
            return;
        }

        foreach (var item in applications)
        {                
            var application = await context.Applications.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Name == item.Name && x.AccountId == accountId);
            if (application == null)
            {
                Application app = new() { SystemName = new Guid(), AccountId = accountId, Name = item.Name, IsActive = item.IsActive };
                await context.Applications.AddAsync(app);
            }
            else
            {
                application.IsActive = true;
                application.Name = item.Name;
                context.Update(application);
            }
        }

        await context.SaveChangesAsync();
    }

    private static async Task CreateModule(ApplicationDbContext context, IReadOnlyCollection<Module> modules, string accountId)
    {
        if (modules is null || !modules.Any())
        {
            return;
        }

        foreach (var item in modules)
        {
            var app = await context.Applications.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.SystemName == item.ApplicationSystemName);
            if (app != null)
            {
                var myReqApp = await context.Applications.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Name == app.Name && x.AccountId == accountId);
                if (myReqApp != null)
                {
                    var module = await context.Modules.FirstOrDefaultAsync(x => x.Name == item.Name && x.ApplicationSystemName == myReqApp.SystemName);
                    if (module == null)
                    {
                        Module md = new()
                        {
                            SystemName = new Guid(),
                            Name = item.Name,
                            IsActive = item.IsActive,
                            ApplicationSystemName = myReqApp.SystemName,
                        };
                        await context.Modules.AddAsync(md);
                    }
                }
            }
        }

        await context.SaveChangesAsync();
    }
}
