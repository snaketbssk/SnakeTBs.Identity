using System;

namespace SnakeTBs.Identity.DataLayer.Models.Tables.Entities
{
    public abstract class GuidUserTable : BaseTable, IGuidUserTable
    {
        public Guid GuidUser { get; set; }
    }
}
