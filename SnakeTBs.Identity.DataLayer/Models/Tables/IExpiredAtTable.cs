using System;

namespace SnakeTBs.Identity.DataLayer.Models.Tables
{
    public interface IExpiredAtTable
    {
        DateTime ExpiredAt { get; set; }
    }
}
