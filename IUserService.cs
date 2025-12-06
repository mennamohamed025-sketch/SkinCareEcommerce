using SkincareEcommerce.Models;

namespace SkincareEcommerce.Interfaces
{
    public interface IUserService
    {
        Task<Customer> RegisterCustomerAsync(Customer customer);
        Task<Admin> RegisterAdminAsync(Admin admin);
        Task<Person> LoginAsync(string email, string password);
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Admin> GetAdminByIdAsync(int id);
    }
}
