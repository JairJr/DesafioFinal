using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc; 
using OrderApi.DTOs; 
using OrderApi.Services.Interfaces; 
 
namespace OrderApi.Controllers; 
 
 [ApiController]
 [Route("api/v1/[controller]")]
 public class OrdersController : ControllerBase 
 { 
     private readonly IOrderService _service; 
 
     public OrdersController(IOrderService service) 
     { 
         _service = service; 
     } 
 /// <summary>
 /// Obtem todos os pedidos
 /// </summary>
 /// <returns>Uma lista de pedidos</returns>
     [HttpGet]
     [SwaggerOperation(Summary = "Obter todos os pedidos", Description = "Retorna uma lista de pedidos disponíveis.")]
     public async Task<IActionResult> GetAll() 
         => Ok(await _service.GetAllAsync()); 
 /// <summary>
 /// Obtem um pedido por ID
 /// </summary>
 /// <param name="id">O ID do pedido</param>
 /// <returns>O pedido correspondente ao ID</returns>
     [HttpGet("{id}")]
     [SwaggerOperation(Summary = "Obter pedido por ID", Description = "Retorna o pedido correspondente ao ID fornecido.")]
     public async Task<IActionResult> GetById(Guid id) 
     { 
         var result = await _service.GetByIdAsync(id); 
         return result == null ? NotFound() : Ok(result); 
     } 
 /// <summary>
 /// Obtem pedidos por nome
 /// </summary>
 /// <param name="name">O nome do pedido</param>
 /// <returns>Uma lista de pedidos que correspondem ao nome</returns>
     [HttpGet("search")]
     [SwaggerOperation(Summary = "Buscar pedidos por nome", Description = "Retorna uma lista de pedidos que correspondem ao nome fornecido.")]

     public async Task<IActionResult> GetByName([FromQuery] string name) 
         => Ok(await _service.GetByNameAsync(name)); 
 /// <summary>
 /// Obtem a contagem total de pedidos
 /// </summary>
 /// <returns>O número total de pedidos</returns>
     [HttpGet("count")]
     [SwaggerOperation(Summary = "Contagem de pedidos", Description = "Retorna o número total de pedidos disponíveis.")]

     public async Task<IActionResult> Count() 
         => Ok(await _service.CountAsync()); 
 /// <summary>
 /// Cria um novo pedido
 /// </summary>
 /// <param name="dto">Os dados do pedido a ser criado</param>
 /// <returns>O pedido criado</returns>
     [HttpPost]
     [SwaggerOperation(Summary = "Criar pedido", Description = "Cria um novo pedido e retorna o recurso criado.")]
     [ProducesResponseType(StatusCodes.Status200OK)]
     [ProducesResponseType(StatusCodes.Status404NotFound)]
     [ProducesResponseType(StatusCodes.Status400BadRequest)]
     public async Task<IActionResult> Create(OrderRequestDto dto)
     {
         try
         {
             var created = await _service.CreateAsync(dto);
             return Ok(created);
         }
         catch (InvalidOperationException ex)
         {
             return NotFound(new { message = ex.Message });
         }
         catch (ArgumentException ex)
         {
             return BadRequest(new { message = ex.Message });
         }
     }
 /// <summary>
 /// Atualiza um pedido existente
 /// </summary>
 /// <param name="id">O ID do pedido a ser atualizado</param>
 /// <param name="dto">Os dados do pedido a serem atualizados</param>
 /// <returns>Indica se a atualização foi bem-sucedida</returns>
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualizar pedido", Description = "Atualiza um pedido existente. Retorna 204 No Content se a atualização for bem-sucedida ou 404 Not Found se o pedido não for encontrado.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
     public async Task<IActionResult> Update(Guid id, OrderRequestDto dto) 
     { 
         var updated = await _service.UpdateAsync(id, dto); 
         return updated ? NoContent() : NotFound(); 
     } 
 /// <summary>
 /// Exclui um pedido por ID
 /// </summary>
 /// <param name="id">O ID do pedido a ser excluído</param>
 /// <returns>Indica se a exclusão foi bem-sucedida</returns>
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Excluir pedido", Description = "Exclui um pedido por ID. Retorna 204 No Content se a exclusão for bem-sucedida ou 404 Not Found se o pedido não for encontrado.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
     public async Task<IActionResult> Delete(Guid id) 
     { 
         var deleted = await _service.DeleteAsync(id); 
         return deleted ? NoContent() : NotFound(); 
     } 
 } 
