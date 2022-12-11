using SQS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

IServiceCollection services = new ServiceCollection();
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json").Build();

// services.AddDbContext<AAtims>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());

services.AddSingleton<ISqsService, SqsService>();
services.AddSingleton(configuration);
ServiceProvider serviceProvider = services.BuildServiceProvider();
IServiceScope scope = serviceProvider.CreateScope();
await scope.ServiceProvider.GetRequiredService<ISqsService>().Run(args);

await serviceProvider.DisposeAsync();