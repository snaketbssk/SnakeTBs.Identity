﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SnakeTBs.Identity.Api.Authentications.Models.Authentication.Entities;
using SnakeTBs.Identity.Api.Models.Entities;
using SnakeTBs.Identity.DataLayer.Models.Contexts.Entities;
using SnakeTBs.Identity.LogicLayer.Exceptions.Entities;
using SnakeTBs.Services.Authentications.SchemeOptions.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SnakeTBs.Identity.Api.Authentications.Handlers.Entities
{
    public class ValidateAuthenticationHandler : BaseAuthenticationHandler<ValidateAuthenticationSchemeOptions>
    {
        public ValidateAuthenticationHandler(
            IOptionsMonitor<ValidateAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            DatabaseContext context)
            : base(options, logger, encoder, clock, context)
        {
        }

        protected override async Task<IEnumerable<Claim>> GetClaimsAsync(string token)
        {
            var guid = Guid.Parse(token);
            var tokenTable = await _context.Tokens
                .Include(v => v.User)
                .FirstOrDefaultAsync(v => v.Guid.Equals(guid));
            if (tokenTable == null) throw new NullTokenException();
            if (!tokenTable.IsActive) throw new IsNotActiveTokenException();
            if (tokenTable.ExpiredAt < DateTime.UtcNow) throw new ExpiredTokenException();
            var userAuthentication = new UserAuthentication(tokenTable);
            var userAgentModel = new UserAgentModel(Request);
            tokenTable = userAgentModel.Update(tokenTable);
            _context.Tokens.Update(tokenTable);
            await _context.SaveChangesAsync();
            var result = userAuthentication.GetClaims();
            return result;
        }
    }
}
