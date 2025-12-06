using SkincareEcommerce.Models;

namespace SkincareEcommerce.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetByCustomerIdAsync(int customerId);
        Task AddItemAsync(CartItem item);
        Task UpdateItemAsync(CartItem item);
        Task RemoveItemAsync(int itemId);
        Task ClearCartAsync(int cartId);
    }
}
