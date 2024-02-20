using Microsoft.AspNetCore.Mvc;
using StoreApi.Model;
using StoreApi.Repository;

namespace StoreApi.Controllers;

[Route("[controller]")]
[ApiController]
public class DeliveryFeeController : ControllerBase
{
    private readonly DeliveryFeeRepository _deliveryFeeRepository;

    public DeliveryFeeController(DeliveryFeeRepository deliveryFeeRepository)
    {
        _deliveryFeeRepository = deliveryFeeRepository;
    }

    #region GET
    [HttpGet("getTaxa")]
    public async Task<ActionResult<DeliveryFee>> GetDeliveryFee()
    {
        try
        {
            var taxa = await _deliveryFeeRepository.ReturnDeliveryFee();

            if (taxa == null)
            {
                return NotFound(new { message = "Taxa de entrega não encontrada." });
            }

            return Ok(taxa);
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
    [HttpPost("createTaxa")]
    public async Task<ActionResult> CreateCategory([FromBody] decimal value)
    {
        try
        {
            if (value < 0)
            {
                return BadRequest(new { message = "Dados inválido." });
            }

            string taxa = await _deliveryFeeRepository.CreateDeliveryFee(value);

            if (taxa == "exist")
            {
                return Conflict(new { message = "Valor já cadastrada." });
            }

            if (taxa == "limit")
            {
                return Conflict(new { message = "Só pode ter um valor cadastrada." });
            }

            return StatusCode(StatusCodes.Status201Created,
                new { message = "Taxa de entrega criado com sucesso." });
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
    [HttpPatch("updateTaxa/{id:int}")]
    public async Task<ActionResult> UpdateDeliveryFee(int id, [FromBody] decimal value)
    {
        try
        {
            if (value < 0)
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            bool taxa = await _deliveryFeeRepository.UpdateDeliveryFee(id, value);

            if (!taxa)
            {
                return NotFound(new { message = "Taxa não encontrada." });
            }

            return Ok(new { message = "Taxa de entrega atualizada com sucesso." });
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
