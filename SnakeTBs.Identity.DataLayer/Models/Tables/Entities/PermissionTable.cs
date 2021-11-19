using System;

namespace SnakeTBs.Identity.DataLayer.Models.Tables.Entities
{
    public class PermissionTable : ConnectionUserTable, IUpdateAtTable
    {
        public DateTime UpdateAt { get; set; }
    }
}
