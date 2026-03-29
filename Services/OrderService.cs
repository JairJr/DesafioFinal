using OrderApi.Data;
using OrderApi.DTOs; 
using OrderApi.Models; 
using OrderApi.Repositories.Interfaces; 
using OrderApi.Services.Interfaces; 
using Microsoft.EntityFrameworkCore;
 
namespace OrderApi.Services; 
 
public class OrderService : IOrderService 
 { 
     private readonly IOrderRepository _repository; 
     private readonly AppDbContext _context;
 
     public OrderService(IOrderRepository repository, AppDbContext context) 
     { 
         _repository = repository; 
         _context = context;
     } 
 
     public async Task<OrderResponseDto> CreateAsync(OrderRequestDto dto) 
     { 
        var product = await _context.Products.FindAsync(dto.ProductId)
            ?? throw new InvalidOperationException("Product not found");

        var customer = await _context.Customers.FindAsync(dto.CustomerId)
            ?? throw new InvalidOperationException("Customer not found");

         var order = new Order 
         { 
             Id = Guid.NewGuid(), 
             CustomerId = dto.CustomerId,
             ProductId = dto.ProductId,
             Quantity = dto.Quantity,
             TotalAmount = product.Price * dto.Quantity
         }; 
 
         var result = await _repository.CreateAsync(order);
         
         // Reload with includes for mapping
         var createdOrder = await _repository.GetByIdAsync(result.Id);
 
         return Map(createdOrder!); 
     } 
 
     public async Task<List<OrderResponseDto>> GetAllAsync() 
         => (await _repository.GetAllAsync()).Select(Map).ToList(); 
 
     public async Task<OrderResponseDto?> GetByIdAsync(Guid id) 
     { 
         var entity = await _repository.GetByIdAsync(id); 
         return entity == null ? null : Map(entity); 
     } 
 
     public async Task<List<OrderResponseDto>> GetByNameAsync(string name) 
         => (await _repository.GetByNameAsync(name)).Select(Map).ToList(); 
 
     public async Task<int> CountAsync() 
         => await _repository.CountAsync(); 
 
     public async Task<bool> UpdateAsync(Guid id, OrderRequestDto dto) 
     { 
         var entity = await _repository.GetByIdAsync(id); 
         if (entity == null) return false; 
        var product = await _context.Products.FindAsync(dto.ProductId) 
            ?? throw new InvalidOperationException("Product not found");

        var customer = await _context.Customers.FindAsync(dto.CustomerId)
            ?? throw new InvalidOperationException("Customer not found");
 
         entity.CustomerId = dto.CustomerId; 
         entity.ProductId = dto.ProductId; 
         entity.Quantity = dto.Quantity;
         entity.TotalAmount = product.Price * dto.Quantity; 
 
         return await _repository.UpdateAsync(entity); 
     } 
 
     public async Task<bool> DeleteAsync(Guid id) 
         => await _repository.DeleteAsync(id); 
 
     private static OrderResponseDto Map(Order order) => new() 
     { 
         Id = order.Id, 
         CustomerId = order.CustomerId,
         CustomerName = order.Customer?.Name ?? "Unknown", 
         ProductId = order.ProductId,
         ProductName = order.Product?.Name ?? "Unknown", 
         Quantity = order.Quantity,
         TotalAmount = order.TotalAmount, 
         CreatedAt = order.CreatedAt 
     }; 
 } 
