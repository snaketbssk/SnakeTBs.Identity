using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SnakeTBs.Identity.Api.Authentications.Models.Constants;
using SnakeTBs.Identity.Api.Controllers.PermissionControllers.Requests.Entities;
using SnakeTBs.Identity.DataLayer.Models.Contexts.Entities;
using SnakeTBs.Identity.DataLayer.Models.Tables.Entities;
using SnakeTBs.Identity.LogicLayer.Models.Constants;
using SnakeTBs.Identity.LogicLayer.Models.Creaters.Entities;
using SnakeTBs.Identity.LogicLayer.Models.Requests.Entities;
using System.Threading.Tasks;

namespace SnakeTBs.Identity.Api.Controllers.PermissionControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class PermissionController : ControllerBase
    {
        private readonly ILogger<PermissionController> _logger;
        private readonly DatabaseContext _context;
        public PermissionController(ILogger<PermissionController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(typeof(PermissionTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> PatchAsync([FromBody] PatchPermissionRequest request)
        {

            var userTable = await _context.Users.FirstOrDefaultAsync(v => v.Guid.Equals(request.Guid));
            var result = await _context.Permissions.FirstOrDefaultAsync(v => v.UserId.Equals(userTable.Id));
            if (result == null)
            {
                var permissionTableCreater = new PermissionTableCreater(userTable.Id);
                var permissionTable = permissionTableCreater.Create();
                var addPermissionTable = await _context.Permissions.AddAsync(permissionTable);
                result = addPermissionTable.Entity;
            }
            else
            {
                var permissionTableCreater = new PermissionTableCreater();
                result = permissionTableCreater.Update(result);
                var updatePermissionTable = _context.Permissions.Update(result);
                result = updatePermissionTable.Entity;
            }
            await _context.SaveChangesAsync();
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("User/{Guid}")]
        [ProducesResponseType(typeof(PermissionTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetAsync([FromRoute] GuidRequest request)
        {
            var userTable = await _context.Users.FirstOrDefaultAsync(v => v.Guid.Equals(request.Guid));
            var result = await _context.Permissions.FirstOrDefaultAsync(v => v.UserId.Equals(userTable.Id));
            return Ok(result);
        }

    }
}
