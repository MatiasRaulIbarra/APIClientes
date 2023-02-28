using APIClientes.Data;
using APIClientes.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIClientes.Repositorio
{
    public class UserRepositorio : IUserRepositorio
    {
        private readonly ApplicationDbContext _db;

        public UserRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }


        public Task<string> Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public async  Task<int> Register(User user, string password)
        {
            try
            {
                if (await UserExiste(user.UserName))
                {
                    return -1;
                }
                CrearPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return user.Id;
            }
            catch (Exception)
            {

                return -500;
            }
        }

        public async Task<bool> UserExiste(string username)
        {
            if (await _db.Users.AnyAsync(x => x.UserName.ToLower().Equals(username.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using ( var hmac  = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
