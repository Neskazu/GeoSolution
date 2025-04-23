using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GeoSolution.Models;

namespace GeoSolution.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUserModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
        public DbSet<CustomBuildingModel> CustomBuildings { get; set; }
        public DbSet<EntranceDataModel> EntranceDatas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=postgres-container;Database=GeoSolution;Username=postgres;Password=qaz741", x=>x.UseNetTopologySuite());
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
