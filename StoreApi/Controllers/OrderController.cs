using Microsoft.AspNetCore.Mvc;
using StoreApi.DTO.Order;
using StoreApi.Model;
using StoreApi.Repository;

namespace StoreApi.Controllers;

[Route("[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderRepository _orderRepository;

    public OrderController(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    #region GET
    [HttpGet("getAllOrders")]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
    {
        try
        {
            var orders = await _orderRepository.ReturnAllOrders();

            if (!orders.Any())
            {
                return NotFound(new { message = "Nenhum pedido foi encontrado." });
            }

            return Ok(orders);
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    [HttpGet("getOrderByDay")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByDay(string day)
    {
        try
        {
            var orders = await _orderRepository.ReturnOrdersByDay(day);

            if (!orders.Any())
            {
                return NotFound(new { message = "Nenhum pedido foi encontrado." });
            }

            return Ok(orders);
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    [HttpGet("getAllOrdersToday")]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrdersToday()
    {
        try
        {
            var orders = await _orderRepository.ReturnOrdersToday();

            if (!orders.Any())
            {
                return NotFound(new { message = "Nenhum pedido foi encontrado." });
            }

            return Ok(orders);
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    [HttpGet("getAllOrdersDone")]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrdersDone()
    {
        try
        {
            var orders = await _orderRepository.ReturnOrdersDone();

            if (!orders.Any())
            {
                return NotFound(new { message = "Nenhum pedido foi encontrado." });
            }

            return Ok(orders);
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    [HttpGet("getOrdersByUser/{id:int}")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserId(int id)
    {
        try
        {
            var orders = await _orderRepository.ReturnOrderByUserId(id);

            if (orders == null)
            {
                return NotFound(new { message = "Nenhum pedido foi encontrado." });
            }

            return Ok(orders);
        }
        catch (Exception)
        {
            return StatusCode(
               StatusCodes.Status500InternalServerError,
                new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    [HttpGet("getCountOrder")]
    public async Task<ActionResult<int>> GetCountCategory()
    {
        try
        {
            int numberOfCategory = await _orderRepository.ReturnNumberOfOrders();

            if (numberOfCategory < 0)
            {
                return NotFound(new { message = "Pedido não encontrado." });
            }

            return Ok(numberOfCategory);
        }
        catch (Exception)
        {
            return StatusCode(
               StatusCodes.Status500InternalServerError,
                new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }
    #endregion

    #region POST
    [HttpPost("createOrder")]
    public async Task<ActionResult<bool>> CreateOrder([FromBody] CreateOrderDTO data)
    {
        try
        {
            bool order = await _orderRepository.CreateOrderByUser(data);

            if (!order)
            {
                return BadRequest(new { message = "Não foi possível fazer o pedido tente novamente mais tarde." });
            }

            return StatusCode(StatusCodes.Status201Created, new { message = "Pedido criado com sucesso." });
        }
        catch (Exception)
        {
            return StatusCode(
               StatusCodes.Status500InternalServerError,
                new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }
    #endregion

    #region UPDATE
    [HttpPatch("updateOrder/{id:int}")]
    public async Task<ActionResult> UpdateOrder(int id, [FromBody] bool done)
    {
        try
        {
            bool order = await _orderRepository.MakeDoneOrder(id, done);

            if (!order)
            {
                return NotFound(new { message = "Nenhum pedido foi encontrado." });
            }

            return Ok(new { message = "Pedido concluído com sucesso." });
        }
        catch (Exception)
        {
            return StatusCode(
               StatusCodes.Status500InternalServerError,
                new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }
    #endregion

    #region DELETE
    [HttpDelete("deleteOrder/{id:int}")]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        try
        {
            bool orderToDelete = await _orderRepository.RemoveOrder(id);

            if (!orderToDelete)
            {
                return NotFound(new { message = "Nenhum pedido foi encontrado." });
            }

            return Ok(new { message = "Pedido excluído com sucesso." });
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }
    #endregion
}
