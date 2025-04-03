﻿using ExtraHours.Core.Models;
using ExtraHours.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace ExtraHours.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() => await _context.Users.ToListAsync();

        public async Task<User?> GetUserByIdAsync(int id) => await _context.Users.FindAsync(id);

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User entity)
        {
            var userExist = await _context.Users
                .FirstOrDefaultAsync(p => p.Id == entity.Id);

            if (userExist == null)
            {
                throw new KeyNotFoundException($"El usuario con ID {entity.Id} no existe.");
            }

            userExist.Name = entity.Name;
            userExist.Email = entity.Email;
            userExist.PhoneNumber = entity.PhoneNumber;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User?> GetByNameOrCodeAsync(string search)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == search || u.Code == search);
        }
    }
}
