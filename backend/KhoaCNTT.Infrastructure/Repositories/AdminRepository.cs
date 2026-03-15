
using KhoaCNTT.Application.Interfaces.Repositories;
using KhoaCNTT.Domain.Entities;
using KhoaCNTT.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KhoaCNTT.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Admin?> GetByUsernameAsync(string username)
        {
            return await _context.Admins
                .FirstOrDefaultAsync(u => u.Username == username);
        }


        public async Task<Admin?> GetByIdAsync(int id)
        {
            return await _context.Admins.FindAsync(id);
        }

        public async Task AddAsync(Admin admin)
        {
            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Admin admin)
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Admin admin)
        {
            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Admin>> GetAllAsync()
        {
            return await _context.Admins.ToListAsync();
        }

        public Task<bool> ExistsAsync(int requesterId)
        {
            throw new NotImplementedException();
        }
    }
}