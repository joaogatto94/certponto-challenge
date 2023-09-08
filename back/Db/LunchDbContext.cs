using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class LunchContext : DbContext
{
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Vote> Votes { get; set; }

    public string DbPath { get; }

    public LunchContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "Lunch.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vote>()
            .HasIndex(v => new {v.Identifier , v.Year, v.Month, v.Day}).IsUnique();

        modelBuilder.Entity<Restaurant>().HasData(
            new Restaurant {RestaurantId = 1, Name = "McDonalds"},
            new Restaurant {RestaurantId = 2, Name = "BK"},
            new Restaurant {RestaurantId = 3, Name = "Sujinho Da Esquina"},
            new Restaurant {RestaurantId = 4, Name = "Limpinho Caro"},
            new Restaurant {RestaurantId = 5, Name = "Delivery"}
        );
    }
}

public class Restaurant
{
    public int RestaurantId { get; set; }
    public string Name { get; set; }

    public List<Vote> Votes { get; } = new();
}

public class Vote
{
    public int VoteId { get; set; }
    public string Identifier { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public int WeekNumber { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
}