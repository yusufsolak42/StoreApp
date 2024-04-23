using System.Reflection;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Config;

namespace Repositories;

public class RepositoryContext : DbContext //represents the database context for the application
{
  public DbSet<Product> Products { get; set; } //tables in my dbcontext. Product Class
  public DbSet<Category> Categories { get; set; } //tables in my dbcontext. Category Class
  public DbSet<Order> Orders { get; set; }//tables in my dbcontext. Order Class


  public RepositoryContext(DbContextOptions<RepositoryContext> options)
  : base(options)
  {

  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    //modelBuilder.ApplyConfiguration(new ProductConfig()); //applies the config file
    //modelBuilder.ApplyConfiguration(new CategoryConfig());//applies the config file

    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //applies the config files automatically..
  }
}