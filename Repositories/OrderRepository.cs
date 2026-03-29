using Microsoft.EntityFrameworkCore; 
using OrderApi.Data; 
using OrderApi.Models; 
using OrderApi.Repositories.Interfaces; 
 
namespace OrderApi.Repositories; 
 
public class OrderRepository : IOrderRepository 
 { 
     private readonly AppDbContext _context; 
 
     public OrderRepository(AppDbContext context) 
     { 
         _context = context; 
     } 
 
     public async Task<Order> CreateAsync(Order order) 
     { 
         _context.Orders.Add(order); 
         await _context.SaveChangesAsync(); 
         return order; 
     } 
 
     public async Task<List<Order>> GetAllAsync() 
         => await _context.Orders
             .Include(o => o.Customer)
             .Include(o => o.Product)
             .ToListAsync(); 
 
     public async Task<Order?> GetByIdAsync(Guid id) 
         => await _context.Orders
             .Include(o => o.Customer)
             .Include(o => o.Product)
             .FirstOrDefaultAsync(o => o.Id == id); 
 
     public async Task<List<Order>> GetByNameAsync(string name) 
         => await _context.Orders 
             .Include(o => o.Customer)
             .Include(o => o.Product)
             .Where(x => x.Customer.Name.Contains(name)) 
             .ToListAsync(); 
 
     public async Task<int> CountAsync() 
         => await _context.Orders.CountAsync(); 
 
     public async Task<bool> UpdateAsync(Order order) 
     { 
         _context.Orders.Update(order); 
         return await _context.SaveChangesAsync() > 0; 
     } 
 
     public async Task<bool> DeleteAsync(Guid id) 
     { 
         var entity = await _context.Orders.FindAsync(id); 
         if (entity == null) return false; 
 
         _context.Orders.Remove(entity); 
         return await _context.SaveChangesAsync() > 0; 
     } 
 } 
