using Domain;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Features.Customers.CreateCustomer;
using Infrastructure.Features.Customers.GetCustomers;
using Infrastructure.Features.Customers.UpdateCustomer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Scopes.ReadUsers)]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets customers
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<User>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomers([FromQuery] GetCustomersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Creates a customer
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Scopes.WriteUsers)]
        [ProducesResponseType(typeof(PagedList<User>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Handle<IActionResult>(Ok, BadRequest);
        }

        /// <summary>
        /// Updates a customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [Authorize(Scopes.WriteUsers)]
        [ProducesResponseType(typeof(PagedList<User>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCustomer(Guid id, UpdateCustomerRequest request)
        {
            var command = new UpdateCustomerCommand(id, request);
            var result = await _mediator.Send(command);
            return result.Handle<IActionResult>(NoContent, BadRequest);
        }
    }
}
