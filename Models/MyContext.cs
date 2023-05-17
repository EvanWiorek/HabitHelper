#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace HabitHelper.Models;

public class MyContext : DbContext
{
public MyContext(DbContextOptions options) : base(options) { }
public DbSet<User> Users { get; set; }
public DbSet<Habit> Habits { get; set; }
public DbSet<Cheer> Cheers { get; set; }
}