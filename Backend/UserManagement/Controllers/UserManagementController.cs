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
            try
            {
               var result = await _userManagementService.Login(userDTO);
               return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<UserDTO>(new UserDTO(), ex.Message));
            }
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterDTO userDTO)
        {
            try
            {
                var result = await _userManagementService.Resgister(userDTO);
                return Ok(result);
         
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<RegisterDTO>(new RegisterDTO(), ex.Message));
            }
        }

        [HttpDelete]

        public async Task<IActionResult> Delete(UserDTO userDTO)
        {
            try
            {
                var result = await _userManagementService.DeleteUser(userDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<UserDTO>(new UserDTO(), ex.Message));
            }
        }
    }
}
