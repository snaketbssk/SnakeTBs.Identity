using SnakeTBs.Identity.DataLayer.Models.Tables.Entities;
using System;

namespace SnakeTBs.Identity.LogicLayer.Models.Creaters.Entities
{
    public class TokenTableCreater : IdCreater<TokenTable>
    {
        public TokenTableCreater(int id) : base(id)
        {
        }
        public override TokenTable Create()
        {
            var result = new TokenTable
            {
                UserId = Id,
                IsActive = true
                //CreatedAt = DateTime.Now
            };
            return result;
        }

        public override TokenTable Update(TokenTable result)
        {
            throw new NotImplementedException();
        }
    }
}
