using System.Text.Json.Serialization;

namespace SnakeTBs.Identity.DataLayer.Models.Tables.Entities
{
    public abstract class ConnectionUserTable : BaseTable
    {
        [JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        public UserTable User { get; set; }
    }
}
