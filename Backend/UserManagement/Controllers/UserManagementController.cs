using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Interface.ServiceInterface;
using UserManagement.Models.DTO;

namespace UserManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;
        
        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDTO userDTO)
        {
           var login = await _userManagementService.Login(userDTO);
            return Ok(login);
        }
    }
}
