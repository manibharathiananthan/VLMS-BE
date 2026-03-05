
using AutoMapper;
using DBContext.cs.Data;
using DBContext.cs.Entity;
using Microsoft.EntityFrameworkCore;
using Service.DTO;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using System.ComponentModel;

namespace Service.Implementation
{



    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       
        public async Task<UserServiceDTO> Register(UserServiceDTO dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                throw new Exception("Email already exists");

            if (await _context.Users.AnyAsync(u => u.Phone == dto.Phone))
                throw new Exception("Phone already exists");

            var user = _mapper.Map<User>(dto);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<UserServiceDTO>(user);
            result.Password = null; 

            return result;
        }


        public async Task<UserServiceDTO?> Login(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return null;

            // SAFE CHECK
            if (string.IsNullOrEmpty(user.PasswordHash))
                return null;

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return _mapper.Map<UserServiceDTO>(user);
        }
        public async Task<List<UserServiceDTO>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            return _mapper.Map<List<UserServiceDTO>>(users);
        }
        public async Task<UserServiceDTO> UpdateUser(UserServiceDTO dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);

            if (user == null)
                return null;

            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.Phone = dto.Phone;

            await _context.SaveChangesAsync();

            return _mapper.Map<UserServiceDTO>(user);
        }
        public async Task<bool> DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return false;

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
