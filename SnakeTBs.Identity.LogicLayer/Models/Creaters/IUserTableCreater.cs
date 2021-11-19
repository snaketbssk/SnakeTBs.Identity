using SnakeTBs.Identity.DataLayer.Models.Tables;
using SnakeTBs.Identity.DataLayer.Models.Tables.Entities;

namespace SnakeTBs.Identity.LogicLayer.Models.Creaters
{
    public interface IUserTableCreater : ICreater<UserTable>
    {
        string Login { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string Phone { get; set; }
        RoleUserTable Role { get; set; }
        LanguageUserTable Language { get; set; }
    }
}
