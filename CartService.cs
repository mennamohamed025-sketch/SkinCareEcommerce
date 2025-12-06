using SkincareEcommerce.Interfaces;
using SkincareEcommerce.Models;

namespace SkincareEcommerce.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<Cart> GetCartByCustomerIdAsync(int customerId)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(customerId);
            if (cart == null)
            {
                cart = new Cart { CustomerId = customerId };
                // Note: Cart creation should be handled in repository or service
            }
            return cart;
        }

        public async Task AddItemToCartAsync(int customerId, int productId, int quantity)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(customerId);
            if (cart == null)
            {
                cart = new Cart { CustomerId = customerId };
                // Assuming cart is created elsewhere
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                await _cartRepository.UpdateItemAsync(existingItem);
            }
            else
            {
                var newItem = new CartItem { ProductId = productId, Quantity = quantity };
                await _cartRepository.AddItemAsync(newItem);
            }
        }

        public async Task UpdateCartItemAsync(int itemId, int quantity)
        {
            var item = new CartItem { Id = itemId, Quantity = quantity };
            await _cartRepository.UpdateItemAsync(item);
        }

        public async Task RemoveItemFromCartAsync(int itemId)
        {
            await _cartRepository.RemoveItemAsync(itemId);
        }

        public async Task ClearCartAsync(int customerId)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(customerId);
            if (cart != null)
            {
                await _cartRepository.ClearCartAsync(cart.Id);
            }
        }
    }
}
