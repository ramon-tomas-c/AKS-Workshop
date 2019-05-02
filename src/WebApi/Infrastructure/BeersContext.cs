namespace WebApi.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using WebApi.Infrastructure.EntityConfigurations;
    using WebApi.Models;

    public class BeersContext : DbContext
    {
        public BeersContext(DbContextOptions<BeersContext> options) : base(options)
        {
        }
        public DbSet<Beer> Beers { get; set; }      

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BeersEntityTypeConfiguration());
        }
    }


    public class BeersContextDesignFactory : IDesignTimeDbContextFactory<BeersContext>
    {
        public BeersContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BeersContext>()
                .UseSqlServer("Server=sql.data;Initial Catalog=BeersDb;User Id=sa;Password=Pass@word");

            return new BeersContext(optionsBuilder.Options);
        }
    }
}
