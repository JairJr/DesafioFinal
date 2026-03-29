namespace OrderApi.DTOs; 
 
public class OrderResponseDto 
 { 
     public Guid Id { get; set; } 
     public Guid CustomerId { get; set; }
     public string CustomerName { get; set; } = string.Empty; 
     public Guid ProductId { get; set; }
     public string ProductName { get; set; } = string.Empty; 
     public int Quantity { get; set; }
     public decimal TotalAmount { get; set; } 
     public DateTime CreatedAt { get; set; } 
 } 
