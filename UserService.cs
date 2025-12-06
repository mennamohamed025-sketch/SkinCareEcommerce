 using SkincareEcommerce.Interfaces;
using SkincareEcommerce.Models;
using SkincareEcommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace SkincareEcommerce.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> RegisterCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Admin> RegisterAdminAsync(Admin admin)
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<Person> LoginAsync(string email, string password)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email && c.Password == password);
            if (customer != null) return customer;

            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
            return admin;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Admin> GetAdminByIdAsync(int id)
        {
            return await _context.Admins.FindAsync(id);
        }
    }
}
