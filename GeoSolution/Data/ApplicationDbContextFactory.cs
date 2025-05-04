namespace GeoSolution.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class ApplicationDbContextFactory
        : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder
                .UseNpgsql(
                   "Host=postgres_container;Database=GeoSolution;Username=postgres;Password=qaz741",
                   o => o.UseNetTopologySuite()
                );

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }

}
