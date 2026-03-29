namespace OrderApi.Models; 
 
public class Order 
 { 
     public Guid Id { get; set; } 
     public Guid CustomerId { get; set; }
     public Customer Customer { get; set; } = null!;
     public Guid ProductId { get; set; }
     public Product Product { get; set; } = null!;
     public int Quantity { get; set; }
     public decimal TotalAmount { get; set; } 
     public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
 } 
