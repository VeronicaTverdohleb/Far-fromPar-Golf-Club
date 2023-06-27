using Microsoft.EntityFrameworkCore;
using Shared.Model;


namespace DataAccess;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../DataAccess/GolfClub.db")
            .EnableSensitiveDataLogging();
    }
    
    // This method configures property constraints in the database
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(user => user.Id); // Setting Id to be the primary key
    }
    
}