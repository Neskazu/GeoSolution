using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GeoSolution.Models;
using GeoSolution.Models.MQ;

namespace GeoSolution.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUserModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
        public DbSet<CustomBuildingModel> CustomBuildings { get; set; }
        public DbSet<EntranceDataModel> EntranceDatas { get; set; }
        public DbSet<LoginEvent> LoginEvents { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=postgres_container;database=GeoSolution;username=postgres;Password=qaz741", x=>x.UseNetTopologySuite());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CustomBuildingModel>(entity =>
            {
                entity.Property(e => e.Geometry).HasColumnType("geometry(MultiPolygon, 4326)");
            });
        }
    }
}
