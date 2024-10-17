using LibraryManagementSystem.DataAccess.DataContext;
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
        public UserRepository(ApplicationContext ctx)
        {
            _ctx = ctx;
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
                response = response.SuccessResultData("User Added Successfully", 200);
            }catch(Exception ex)
            {
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
                    response = response.FailedResultData("User does not exist", 404);
                }
               _ctx.Remove(deleteuser!);
                await _ctx.SaveChangesAsync();
                response = response.SuccessResultData("User deleted successfully");
            }catch(Exception ex)
            {
                response = response.FailedResultData(ex.Message);
            }
            return response;
        }

        public Task<List<User>> GetAllUser()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDetail<string>> GetUserById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDetail<string>> UpdateUser(UpdateUserDTO userDTO)
        {
            throw new NotImplementedException();
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
                return $"Memb-{user.Count}++";

            }

        }
    }
}
