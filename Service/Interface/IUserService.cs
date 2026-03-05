using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserService
    {
        Task<UserServiceDTO> Register(UserServiceDTO dto);
        Task<UserServiceDTO> Login(string email, string password);
        Task<List<UserServiceDTO>> GetUsers();
        Task<UserServiceDTO> UpdateUser(UserServiceDTO dto);  

        Task<bool> DeleteUser(int userId);
    }
}
