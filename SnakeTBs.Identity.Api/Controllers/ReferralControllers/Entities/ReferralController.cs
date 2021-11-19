using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SnakeTBs.Identity.Api.Authentications.Models.Authentication.Entities;
using SnakeTBs.Identity.Api.Authentications.Models.Constants;
using SnakeTBs.Identity.DataLayer.Models.Contexts.Entities;
using SnakeTBs.Identity.DataLayer.Models.Tables.Entities;
using SnakeTBs.Identity.LogicLayer.Models.Constants;
using SnakeTBs.Identity.LogicLayer.Models.Creaters.Entities;
using SnakeTBs.Identity.LogicLayer.Models.Requests.Entities;
using System.Threading.Tasks;

namespace SnakeTBs.Identity.Api.Controllers.ReferralControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class ReferralController : ControllerBase
    {
        private readonly ILogger<ReferralController> _logger;
        private readonly DatabaseContext _context;
        public ReferralController(ILogger<ReferralController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// [IDENTITY]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ReferralTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.IDENTITY)]
        public async Task<IActionResult> GetAsync()
        {
            var userAuthentication = new UserAuthentication(HttpContext.User);
            var result = await _context.Referrals
                .Include(v => v.Users)
                .FirstOrDefaultAsync(v => v.UserId.Equals(userAuthentication.Id));
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("User/{Guid}")]
        [ProducesResponseType(typeof(ReferralTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetAsync([FromRoute] GuidRequest request)
        {
            var userTable = await _context.Users.FirstOrDefaultAsync(v => v.Guid.Equals(request.Guid));
            var result = await _context.Referrals
                .Include(v => v.Users)
                .FirstOrDefaultAsync(v => v.UserId.Equals(userTable.Id));
            if (result == null)
            {
                var referralTableCreater = new ReferralTableCreater(userTable.Id);
                result = referralTableCreater.Create();
            }
            return Ok(result);
        }
    }
}
