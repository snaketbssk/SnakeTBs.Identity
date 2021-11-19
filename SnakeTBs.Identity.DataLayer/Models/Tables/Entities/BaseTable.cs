﻿using Microsoft.EntityFrameworkCore;
using SnakeTBs.Identity.DataLayer.Models.Contexts.Constants;
using System;
using System.Text.Json.Serialization;

namespace SnakeTBs.Identity.DataLayer.Models.Tables.Entities
{
    public abstract class BaseTable : IIdTable, IGuidTable, ICreatedAtTable
    {
        [JsonIgnore]
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? Guid { get; set; }
        public static void OnModelCreating<T>(ModelBuilder modelBuilder)
            where T : BaseTable
        {
            modelBuilder.Entity<T>().Property(v => v.Guid).HasDefaultValueSql(ContextConstant.NEW_GUID);
            modelBuilder.Entity<T>().HasIndex(v => v.Guid).IsUnique();
            modelBuilder.Entity<T>().Property(v => v.CreatedAt).HasDefaultValueSql(ContextConstant.GET_UTC_DATE);
        }
    }
}
