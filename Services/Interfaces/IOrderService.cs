using OrderApi.DTOs; 
 
namespace OrderApi.Services.Interfaces; 
 
public interface IOrderService 
 { 
     Task<OrderResponseDto> CreateAsync(OrderRequestDto dto); 
     Task<List<OrderResponseDto>> GetAllAsync(); 
     Task<OrderResponseDto?> GetByIdAsync(Guid id); 
     Task<List<OrderResponseDto>> GetByNameAsync(string name); 
     Task<int> CountAsync(); 
     Task<bool> UpdateAsync(Guid id, OrderRequestDto dto); 
     Task<bool> DeleteAsync(Guid id); 
 } 
