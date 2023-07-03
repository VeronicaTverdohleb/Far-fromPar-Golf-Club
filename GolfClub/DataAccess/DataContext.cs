using Microsoft.EntityFrameworkCore;
using Shared.Model;


namespace DataAccess;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Hole> Holes { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../DataAccess/GolfClub.db")
            .EnableSensitiveDataLogging();
    }
    
    // This method configures property constraints in the database
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(user => user.Id); // Setting Id to be the primary key
        modelBuilder.Entity<Equipment>().HasKey(equipment => equipment.Name);
        modelBuilder.Entity<Game>().HasKey(game => game.Id);
        modelBuilder.Entity<Hole>().HasKey(hole => hole.Number); 
        modelBuilder.Entity<Lesson>().HasKey(lesson => lesson.Id);
        modelBuilder.Entity<Score>().HasKey(score => score.Id); 
        modelBuilder.Entity<Tournament>().HasKey(tournament => tournament.Name);
    }
}