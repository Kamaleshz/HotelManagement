using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Interfaces.ServiceInterface;
using UserManagement.Models;
using UserManagement.Models.DTO;

namespace UserManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var result = await _roleService.GetRoles();
                return Ok(new Response<ICollection<Role>>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Role>(new Role(), ex.Message));
            }
        }
    }
}
