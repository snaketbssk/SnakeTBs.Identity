using System;

namespace SnakeTBs.Identity.DataLayer.Models.Tables
{
    public interface ICreatedAtTable
    {
        DateTime? CreatedAt { get; set; }
    }
}
