using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RecipeBox.Models
{
  public class RecipeBoxContextFactory : IDesignTimeDbContextFactory<RecipeBoxContext>
  {
    RecipeBoxContext IDesignTimeDbContextFactory<RecipeBoxContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();
      
      var builder = new DbContextOptionsBuilder<RecipeBoxContext>();
      string ConnectionString = configuration.GetConnectionString("Defaultconnection");

      builder.UseMySql(ConnectionString);

      return new RecipeBoxContext(builder.Options);
    }
  }
}