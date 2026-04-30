
using Microsoft.EntityFrameworkCore;
using PlServer.Server.Infrastructure.Persistence.Entities;

namespace PlServer.Server.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<UserEntity> Users => Set<UserEntity>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<UserEntity>()
            .HasIndex(x => x.Name);

        modelBuilder.Entity<UserEntity>()
            .Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}
