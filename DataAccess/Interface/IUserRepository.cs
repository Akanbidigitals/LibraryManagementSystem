using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Model.DTOs.User;

namespace LibraryManagementSystem.DataAccess.Interface
{
    public interface IUserRepository
    {
        Task<ResponseDetail<string>> AddUser(AddUserDTO userDTO);
        Task<ResponseDetail<string>> UpdateUser(UpdateUserDTO userDTO);
        Task<ResponseDetail<string>> DeleteUser(Guid Id);
        Task<ResponseDetail<string>> GetUserById(Guid Id);
        Task<List<User>> GetAllUser();
    }
}
