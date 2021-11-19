﻿using Microsoft.EntityFrameworkCore;
using SnakeTBs.Identity.DataLayer.Models.Contexts.Entities;
using SnakeTBs.Identity.DataLayer.Models.Tables.Entities;
using SnakeTBs.Identity.LogicLayer.Exceptions.Entities;
using SnakeTBs.Identity.LogicLayer.Models.Creaters;
using SnakeTBs.Identity.LogicLayer.Models.Creaters.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnakeTBs.Identity.LogicLayer.Managers.Entities
{
    public class UserManager : Manager<DatabaseContext>
    {
        public UserManager(DatabaseContext context) : base(context)
        {
        }
        public async Task<UserTable> CreateAsync(IUserTableCreater userTableCreater, Guid? Referral, bool isReferral, bool isBalance)
        {
            var loginUserTable = await _context.Users.FirstOrDefaultAsync(v => v.Login.Equals(userTableCreater.Login));
            if (loginUserTable != null) throw new ExistsUserException("Login");
            var emailUserTable = await _context.Users.FirstOrDefaultAsync(v => v.Email.Equals(userTableCreater.Login));
            if (emailUserTable != null) throw new ExistsUserException("Email");
            var userTable = userTableCreater.Create();
            var addUserTable = await _context.Users.AddAsync(userTable);
            if (Referral.HasValue)
            {
                var referralTable = await _context.Referrals
                    .Include(v => v.Users)
                    .FirstOrDefaultAsync(v => v.Guid.Equals(Referral.Value));
                if (referralTable != null)
                {
                    if (referralTable.Users == null) referralTable.Users = new List<UserTable>();
                    referralTable.Users.Add(addUserTable.Entity);
                }
            }
            if (isReferral)
            {
                var referralTableCreater = new ReferralTableCreater(addUserTable.Entity.Id);
                var referralTable = referralTableCreater.Create();
                referralTable.User = addUserTable.Entity;
                var addReferralTable = await _context.Referrals.AddAsync(referralTable);
            }
            if (isBalance)
            {
            }
            await _context.SaveChangesAsync();
            return addUserTable.Entity;
        }
    }
}
