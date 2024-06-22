
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using GlideGo_Backend.API.Design.Domain.Model.Aggregates;
using GlideGo_Backend.API.Execution_Monitor.Domain.Model.Entities;
using GlideGo_Backend.API.IAM.Domain.Model.Aggregates;
using GlideGo_Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GlideGo_Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext : DbContext
{
    public DbSet<VehicleUsage> VehicleUsages { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        builder.AddCreatedUpdatedInterceptor();
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();

        builder.Entity<Vehicle>().ToTable("vehicles");
        builder.Entity<Vehicle>().HasKey(v => v.Id);
        builder.Entity<Vehicle>().Property(v=> v.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Vehicle>().Property(v => v.Category).IsRequired().HasMaxLength(30);
        builder.Entity<Vehicle>().Property(v => v.SubCategory).IsRequired().HasMaxLength(30);
        builder.Entity<Vehicle>().Property(v => v.IdVehicle).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Vehicle>().Property(v => v.IdOwner).IsRequired();
        
       
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}