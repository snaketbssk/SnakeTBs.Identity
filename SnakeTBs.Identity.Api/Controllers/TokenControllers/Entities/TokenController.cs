﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SnakeTBs.Identity.Api.Authentications.Models.Authentication.Entities;
using SnakeTBs.Identity.Api.Authentications.Models.Constants;
using SnakeTBs.Identity.Api.Controllers.TokenControllers.Exceptions.Entities;
using SnakeTBs.Identity.Api.Controllers.TokenControllers.Requests.Entities;
using SnakeTBs.Identity.Api.Models.Entities;
using SnakeTBs.Identity.DataLayer.Models.Contexts.Entities;
using SnakeTBs.Identity.DataLayer.Models.Tables;
using SnakeTBs.Identity.DataLayer.Models.Tables.Entities;
using SnakeTBs.Identity.LogicLayer.Exceptions.Entities;
using SnakeTBs.Identity.LogicLayer.Models.Constants;
using SnakeTBs.Identity.LogicLayer.Models.Creaters.Entities;
using SnakeTBs.Identity.LogicLayer.Models.Requests.Entities;
using System.Threading.Tasks;

namespace SnakeTBs.Identity.Api.Controllers.TokenControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly DatabaseContext _context;
        public TokenController(ILogger<TokenController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// [ANY]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(TokenTable), 200)]
        public async Task<IActionResult> PutSignInAsync([FromBody] SignInRequest request)
        {

            string exception = "Login";
            UserTable userTable = null;
            if (request.LoginOrEmail.Contains("@"))
            {
                userTable = await _context.Users.FirstOrDefaultAsync(v => v.Email.Equals(request.LoginOrEmail));
                exception = "Email";
            }
            else userTable = await _context.Users.FirstOrDefaultAsync(v => v.Login.Equals(request.LoginOrEmail));
            if (userTable == null) throw new NullUserException(exception);
            if (userTable.Role == RoleUserTable.Service) throw new SignInServiceUserException();
            if (userTable.Role == RoleUserTable.Bot) throw new SignInBotUserException();
            if (!userTable.Password.Equals(request.Password)) throw new WrongPasswordUserException();
            var xTokenTableCreater = new TokenTableCreater(userTable.Id);
            var xTokenTable = xTokenTableCreater.Create();
            var userAgentModel = new UserAgentModel(Request);
            xTokenTable = userAgentModel.Update(xTokenTable);
            var result = await _context.Tokens.AddAsync(xTokenTable);
            await _context.SaveChangesAsync();
            return Ok(result.Entity);
        }
        /// <summary>
        /// [IDENTITY]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(TokenTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.IDENTITY)]
        public async Task<IActionResult> GetAsync()
        {
            var userAuthentication = new UserAuthentication(HttpContext.User);
            var result = await _context.Tokens.FirstOrDefaultAsync(v => v.Guid.Equals(userAuthentication.Token));
            return Ok(result);
        }
        /// <summary>
        /// [HUMAN]
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.HUMAN)]
        public async Task<IActionResult> DeleteAsync()
        {
            var userAuthentication = new UserAuthentication(HttpContext.User);
            var result = await _context.Tokens.FirstOrDefaultAsync(v => v.Guid.Equals(userAuthentication.Token));
            _context.Tokens.Remove(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("{Guid}")]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> DeleteAsync([FromRoute] GuidRequest request)
        {
            var result = await _context.Tokens.FirstOrDefaultAsync(v => v.Guid.Equals(request.Guid));
            _context.Tokens.Remove(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }
    }
}
