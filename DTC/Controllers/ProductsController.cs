using Dtc.Application.Commands.Products;
using Dtc.Application.Queries.Products;
using Dtc.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DTC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly IMediator _mediator;
        private ILogger<ProductsController> _logger;

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Consultar todos os produtos.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Retorna os dados dos produtos</response>
        ///  <response code ="204">Nenhum produtos foi encontrado</response>
        [HttpGet("getAll")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
        {
            var response = await _mediator.Send(new GetAllProductsQuery());

            if (response is null || !response.Any())
                return NotFound($"Produtos não encontrados.");

            return Ok(response);
        }


        /// <summary>
        /// Busca o produto atraves do Id.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Retorna os dados do produto</response>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest("O ID precisa ser maior que zero.");
            var response = await _mediator.Send(new GetProductByIdQuery(id));

            if (response is null)
                return NotFound($"Produto com ID {id} não encontrado.");

            return Ok(response);
        }


        /// <summary>
        /// Cria um novo produto e envia para Kafka criar no mongoDb.
        /// </summary>
        /// <param name="request">Dados do produto a ser criado.</param>
        /// <returns>O produto criado.</returns>
        /// <response code="201">Produto criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro interno.</response>
        [HttpPost("products")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductCommand request)
        {
            try
            {
                var product = await _mediator.Send(request);
                return StatusCode(StatusCodes.Status201Created, product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar produto.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar produto.");
            }
        }
    }
}
