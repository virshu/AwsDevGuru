using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace VotesData;

public class VotesContext : DbContext
{
    public virtual DbSet<Votes> Votes { get; set; }
    public VotesContext()
    {
            
    }

    public VotesContext(DbContextOptions<VotesContext> options) : base(options)
    {
            
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.development.json", optional: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }
}
