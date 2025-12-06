using SkincareEcommerce.Interfaces;
using SkincareEcommerce.Models;

namespace SkincareEcommerce.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<Order> CreateOrderFromCartAsync(int customerId)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(customerId);
            if (cart == null || !cart.Items.Any())
            {
                throw new InvalidOperationException("Cart is empty.");
            }

            // Check stock
            foreach (var item in cart.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product.StockQuantity < item.Quantity)
                {
                    throw new InvalidOperationException($"Insufficient stock for {product.Name}.");
                }
            }

            // Create order
            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                TotalAmount = cart.Items.Sum(i => i.Quantity * i.Product.Price),
                Status = "Pending",
                Items = cart.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.Product.Price
                }).ToList()
            };

            await _orderRepository.AddAsync(order);

            // Update stock
            foreach (var item in cart.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                product.StockQuantity -= item.Quantity;
                await _productRepository.UpdateAsync(product);
            }

            // Clear cart
            await _cartRepository.ClearCartAsync(cart.Id);

            return order;
        }

        public async Task ProcessPaymentAsync(int orderId, string paymentMethod)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new InvalidOperationException("Order not found.");
            }

            // Mock payment processing
            var payment = new Payment
            {
                OrderId = orderId,
                Amount = order.TotalAmount,
                PaymentMethod = paymentMethod,
                Status = "Completed"
            };

            // Assuming Payment is added via repository, but for simplicity, add to order
            order.Payment = payment;
            order.Status = "Paid";

            await _orderRepository.UpdateAsync(order);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId)
        {
            return await _orderRepository.GetByCustomerIdAsync(customerId);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }
    }
}
