﻿using LibraryManagementSystem.DataAccess.DataContext;
using LibraryManagementSystem.DataAccess.Interface;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Model.DTOs.User;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _ctx;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(ApplicationContext ctx, ILogger<UserRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;

        }
        public async Task<ResponseDetail<string>> AddUser(AddUserDTO userDTO)
        {
            var response = new ResponseDetail<string>();    
            try
            {
                var user = new User()
                {
                    FirstName = userDTO.FirstName,
                    LasttName = userDTO.LasttName,
                    Email = userDTO.Email,
                    MemebershipCode = GenerateMemebrshipcode(),
                    PhoneNumber =  userDTO.PhoneNumber,
                };

                await _ctx.Users.AddAsync(user);
                await _ctx.SaveChangesAsync();
                _logger.LogInformation("User Added sucessfully");
                response = response.SuccessResultData("User Added Successfully", 200);

            }catch(Exception ex)
            {
                _logger.LogError(message: "Error Adding new user");
                response = response.FailedResultData(ex.Message);
            }
            return response;
        }

        public async Task<ResponseDetail<string>> DeleteUser(Guid Id)
        {
            var response = new ResponseDetail<string>();
            try
            {
                var deleteuser = await _ctx.Users.FirstOrDefaultAsync( x=> x.Id == Id);
                if(deleteuser == null)
                {
                    _logger.LogError(message: "User id does not exist");
                    response = response.FailedResultData("User does not exist", 404);
                }
               _ctx.Remove(deleteuser!);
                await _ctx.SaveChangesAsync();
                _logger.LogInformation("User deleted sucessfully");
                response = response.SuccessResultData("User deleted successfully");
            }catch(Exception ex)
            {
                _logger.LogError(message: "Error Deleting  user");
                response = response.FailedResultData(ex.Message);
            }
            return response;
        }

        public async Task<List<User>> GetAllUser()
        {
            try
            {
                var getall = await _ctx.Users.ToListAsync();
                _logger.LogInformation("User deleted sucessfully");
                return getall;
            }
            catch
            {
                _logger.LogError(message: "Error getting all user");
                throw;
            }
        }

        public async Task<ResponseDetail<string>> GetUserById(Guid Id)
        {
            var response = new ResponseDetail<string>();
            try
            {
                var getuser = await _ctx.Users.FirstOrDefaultAsync(x => x.Id == Id);
                if(getuser == null)
                {
                    _logger.LogError(message: "User id does not exist");
                    response = response.FailedResultData("User does not exist");
                }
                _logger.LogInformation("User sucessfully gotten");
                response = response.SuccessResultData($"{getuser}", 200);
            }
            catch(Exception ex)
            {
                _logger.LogError(message: "Error getting user by Id");
                response = response.FailedResultData(ex.Message);
            }
            return response;
        }

        public async Task<ResponseDetail<string>> UpdateUser(UpdateUserDTO userDTO)
        {
            var response = new ResponseDetail<string>();
            try
            {
                var updateUser = await _ctx.Users.FirstOrDefaultAsync(x => x.Id == userDTO.Id);
                if(updateUser.Id == null)
                {
                    _logger.LogError(message: "User id does not exist");
                    response = response.FailedResultData("User does not exist");

                }
                updateUser.Id = userDTO.Id;
                updateUser.FirstName = userDTO.FirstName ?? updateUser.FirstName;
                updateUser.LasttName = userDTO.LasttName ?? updateUser.LasttName;
                updateUser.Email = userDTO.Email ?? updateUser.Email;
                updateUser.PhoneNumber = userDTO.PhoneNumber ?? updateUser.PhoneNumber;
                updateUser.MemebershipCode = updateUser.MemebershipCode;

                _ctx.Users.Update(updateUser);
                await _ctx.SaveChangesAsync();
                _logger.LogInformation("User updated successfully");
                response = response.SuccessResultData("User updated successfully", 200);

            }catch( Exception ex )
            {
                _logger.LogError(message: "Error updating user");
                response = response.FailedResultData(ex.Message);

            }
            return response;
        }

        private  string GenerateMemebrshipcode()
        {
            var user =  _ctx.Users.ToList();
            if(user.Count == 1)
            {
                return $"Memb-{user.Count}";
            }
            else
            {
                return $"Memb-{user.Count}";

            }

        }
    }
}
