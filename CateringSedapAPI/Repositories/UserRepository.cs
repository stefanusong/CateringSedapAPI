﻿using CateringSedapAPI.Context;
using CateringSedapAPI.Entitties;
using Microsoft.EntityFrameworkCore;

namespace CateringSedapAPI.Repositories
{
    public interface IUserRepository 
    {
        Task<User?> GetUserByUsername(string username);
        Task<Guid> CreateUser(User user);
    }

    public class UserRepository : IUserRepository
    {
        public readonly ApplicationContext _db;

        public UserRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            var user = await _db.Users.Where(x => x.Username == username.ToLower()).FirstOrDefaultAsync();
            return user;
        }

        public async Task<Guid> CreateUser(User user)
        {
            var res =  _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return res.Entity.Id;
        }
    }
}
