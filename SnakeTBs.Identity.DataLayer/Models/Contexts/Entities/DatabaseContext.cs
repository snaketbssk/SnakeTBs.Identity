using Microsoft.EntityFrameworkCore;
using SnakeTBs.Extensions.Encryptors.Entities;
using SnakeTBs.Identity.DataLayer.Models.Tables;
using SnakeTBs.Identity.DataLayer.Models.Tables.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeTBs.Identity.DataLayer.Models.Contexts.Entities
{
    public class DatabaseContext : DbContext
    {
        internal static SettingsContext Settings { get; private set; } = new SettingsContext();
        public DbSet<UserTable> Users { get; set; }
        public DbSet<ReferralTable> Referrals { get; set; }
        public DbSet<TokenTable> Tokens { get; set; }
        public DbSet<PermissionTable> Permissions { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public static DatabaseContext GetContext()
        {
            var result = new DatabaseContext(Settings.Options);
            return result;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BaseTable.OnModelCreating<UserTable>(modelBuilder);
            modelBuilder.Entity<UserTable>().HasIndex(v => v.Login).IsUnique();
            modelBuilder.Entity<UserTable>().HasIndex(v => v.Email).IsUnique();
            BaseTable.OnModelCreating<TokenTable>(modelBuilder);
            BaseTable.OnModelCreating<ReferralTable>(modelBuilder);
            BaseTable.OnModelCreating<PermissionTable>(modelBuilder);
            //
            var users = new List<UserTable>();
            users.Add(new UserTable
            {
                Id = users.Count + 1,
                Login = "admin",
                Email = "admin@example.com",
                Password = MD5Encryptor.Encrypt("admin"),
                Role = RoleUserTable.Admin,
                CreatedAt = DateTime.MinValue,
                Guid = Guid.Parse("82a700e7-945d-4bc2-a05a-b5b1fddb423c"),
            });
            var tokens = new List<TokenTable>();
            tokens.Add(new TokenTable
            {
                Id = tokens.Count + 1,
                UserId = users.First(v => v.Login.Equals("admin")).Id,
                IsActive = true,
                CreatedAt = DateTime.MinValue,
                ExpiredAt = DateTime.MaxValue,
                Guid = Guid.Parse("7893d448-7fc5-4fdf-b2f0-7d68a3d190d7"),
            });
            //
            modelBuilder.Entity<UserTable>().HasData(users);
            modelBuilder.Entity<TokenTable>().HasData(tokens);
        }
    }
}
