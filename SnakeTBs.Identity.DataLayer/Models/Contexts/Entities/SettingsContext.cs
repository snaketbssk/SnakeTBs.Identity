using Microsoft.EntityFrameworkCore;
using SnakeTBs.Configurations.Entities;
using SnakeTBs.Identity.Configurations.Models.Branches.Entities;

namespace SnakeTBs.Identity.DataLayer.Models.Contexts.Entities
{
    public class SettingsContext
    {
        public const string DECIMAL = "decimal(18,8)";
        public const string NEW_GUID = "NEWID()";
        public DbContextOptionsBuilder<DatabaseContext> Builder { get; set; }
        public DbContextOptions<DatabaseContext> Options { get; set; }
        public SettingsContext()
        {
            Builder = new DbContextOptionsBuilder<DatabaseContext>();
            Builder.UseSqlServer(BranchConfiguration<RootBranch>.Instance.Root.Database.Connection);
            Options = Builder.Options;
        }
    }
}
