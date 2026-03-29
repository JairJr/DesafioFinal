using OrderApi.Models; 
 
namespace OrderApi.Repositories.Interfaces; 
 
public interface IOrderRepository 
 { 
     Task<Order> CreateAsync(Order order); 
     Task<List<Order>> GetAllAsync(); 
     Task<Order?> GetByIdAsync(Guid id); 
     Task<List<Order>> GetByNameAsync(string name); 
     Task<int> CountAsync(); 
     Task<bool> UpdateAsync(Order order); 
     Task<bool> DeleteAsync(Guid id); 
 } 
