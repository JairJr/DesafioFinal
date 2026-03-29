using Microsoft.EntityFrameworkCore; 
using OrderApi.Data; 
using OrderApi.DependencyInjection; 
using OrderApi.Models;
using System.Reflection;
using System.IO;
 
var builder = WebApplication.CreateBuilder(args); 
 
builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath)) c.IncludeXmlComments(xmlPath);
}); 
 
builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); 
 
builder.Services.AddServices(); 
 
var app = builder.Build(); 
 
// Ensure the database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // For production, use dbContext.Database.Migrate();
    // For simplicity in this demo, EnsureCreated() works for initial setup
    await dbContext.Database.EnsureCreatedAsync();

    // Seed initial data if empty
    if (!await dbContext.Customers.AnyAsync())
    {
        var customer = new Customer { Id = Guid.NewGuid(), Name = "João Silva", Email = "joao@email.com" };
        var product = new Product { Id = Guid.NewGuid(), Name = "Notebook", Price = 4500.00m };
        
        dbContext.Customers.Add(customer);
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
    }
}

app.UseSwagger(); 
app.UseSwaggerUI(); 
 
app.UseHttpsRedirection(); 
app.UseAuthorization(); 
 
app.MapControllers(); 
 
app.Run(); 
