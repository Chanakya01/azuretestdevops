using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Valuelabs.API.Models;

namespace Valuelabs.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string Username, string Password)
        {
            var user =await _context.Users.FirstOrDefaultAsync(x => x.Username == Username);
            if(user == null)
                return null;
            if(!VerifyPasswordHash(Password,user.PasswordHash,user.PasswordSalt))
                return null;
            
            return user;
        }

        private bool VerifyPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt)
        {
             using(var hmac = new System.Security.Cryptography.HMACSHA512(PasswordSalt))
            {
               
              var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
              for (int i=0;i<computedHash.Length;i++)
              {
                  if(computedHash[i]!=PasswordHash[i])
                    return false;
              }
            }
            return true;
        }

        public async Task<User> RegisterAsync(User user, string Password)
        {
            byte[] PasswordHash, PasswordSalt;
            CreatePasswordHash(Password, out PasswordHash, out PasswordSalt);
            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            if(await _context.Users.AnyAsync(x => x.Username == username))
                return true;
            return false;    
        }
    }
}