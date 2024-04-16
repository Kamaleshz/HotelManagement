using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Interface.ServiceInterface;
using UserManagement.Models.DTO;

namespace UserManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("Corspolicy")]
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
               return Ok(new Response<UserDTO>(result));
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
                return Ok(new Response<UserDTO>(result));
         
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
                return Ok(new Response<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<UserDTO>(new UserDTO(), ex.Message));
            }
        }

        [HttpPut]

        public async Task<IActionResult> UpdatePassword(UserDTO resgisteeDTO)
        {
            try
            {
                var result = await _userManagementService.UpdatePassword(resgisteeDTO);
                return Ok(new Response<string>(result));
            }
            catch(Exception ex)
            {
                return BadRequest(new Response<string>(new string(""), ex.Message));
            }
        }

        [HttpPut]

        public async Task<IActionResult> UpdateUserDetails(UserDTO userDTO)
        {
            try
            {
                var result = await _userManagementService.UpdateUser(userDTO);
                return Ok(new Response<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>(new string(""),ex.Message));
            }
        }
    }
}
