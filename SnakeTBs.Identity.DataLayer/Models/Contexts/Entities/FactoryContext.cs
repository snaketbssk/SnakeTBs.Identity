using Microsoft.EntityFrameworkCore.Design;
using SnakeTBs.Identity.Configurations.Entities;

namespace SnakeTBs.Identity.DataLayer.Models.Contexts.Entities
{
    public class FactoryContext : IDesignTimeDbContextFactory<DatabaseContext>
    {
        DatabaseContext IDesignTimeDbContextFactory<DatabaseContext>.CreateDbContext(string[] args)
        {
            AppConfiguration.Set(args);
            var result = DatabaseContext.GetContext();
            return result;
        }
    }
}
