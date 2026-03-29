using OrderApi.Repositories; 
using OrderApi.Repositories.Interfaces; 
using OrderApi.Services; 
using OrderApi.Services.Interfaces; 
 
namespace OrderApi.DependencyInjection; 
 
 public static class Dependencies 
 { 
     public static IServiceCollection AddServices(this IServiceCollection services) 
     { 
         services.AddScoped<IOrderService, OrderService>(); 
         services.AddScoped<IOrderRepository, OrderRepository>(); 
         return services; 
     } 
 } 
