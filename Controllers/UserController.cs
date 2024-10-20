using LibraryManagementSystem.DataAccess.Interface;
using LibraryManagementSystem.Model.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(AddUserDTO dto)
        {
            try
            {
                var res = await _userRepo.AddUser(dto);
                return Ok(res);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
        
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var res = await _userRepo.GetAllUser();
                return Ok(res);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
        
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserByID(Guid Id)
        {
            try
            {
                var res = await _userRepo.GetUserById(Id);
                return Ok(res);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
        
        [HttpDelete("DeleteUserById")]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            try
            {
                var res = await _userRepo.DeleteUser(Id);
                return Ok(res);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO dto)
        {
            try
            {
                var res = await _userRepo.UpdateUser(dto);
                return Ok(res);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}
