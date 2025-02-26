using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<UserEntity> InsertAsync(UserEntity user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
   

        public async Task<UserEntity> UpdateUserAsync(UserEntity user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }


   

    


        public async Task<UserEntity> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            // Implement logic to retrieve user from the database
            //var user = await _context.Users.FirstOrDefaultAsync();
            //.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == HashPassword(password)); // Ensure password is hashed

            var user = new UserEntity{Email = "ana@teste.com", PasswordHash =  HashPassword("Fictici@08"), HierarchyLevel = 1};
            return user;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
