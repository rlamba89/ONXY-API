using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onyx.Contracts.DTO;
using Onyx.Core.Exceptions;
using Onyx.Core.Handlers.Queries;

namespace Onyx.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        public IActionResult Get()
        {
            return Ok();
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [Route("api/products")]
        public async Task<IActionResult> GetProducts()
        {
            var query = new GetAllProductsQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route("api/products/{colour}")]
        [ProducesResponseType(typeof(ProductDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> GetByColour(string colour)
        {
            try
            {
                var query = new GetProductByColourQuery(colour);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }
    }
}