namespace OrderApi.DTOs; 
 
public class OrderRequestDto 
 { 
     public Guid CustomerId { get; set; } 
     public Guid ProductId { get; set; } 
     public int Quantity { get; set; }
 } 
