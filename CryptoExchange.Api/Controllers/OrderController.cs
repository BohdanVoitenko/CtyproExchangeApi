using System;
using AutoMapper;
using CryptoExchange.Api.Models;
using CryptoExchange.Application.Common.Caching;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Application.Orders.Commands.CreateFromXmlFile;
using CryptoExchange.Application.Orders.Commands.CreateOrder;
using CryptoExchange.Application.Orders.Commands.DeleteOrder;
using CryptoExchange.Application.Orders.Commands.UpdateOrder;
using CryptoExchange.Application.Orders.Queries.GetAllOrdersByExchangerQuery;
using CryptoExchange.Application.Orders.Queries.GetAllOrdersQuery;
using CryptoExchange.Application.Orders.Queries.GetOrderDetails;
using CryptoExchange.Application.Orders.Queries.GetOrderList;
using CryptoExchange.Application.Orders.Queries.GetOrderListByCoins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
	[Route("api/[controller]")]
	public class OrderController : BaseController
	{
		private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;
        private readonly IUserService _userService;

		public OrderController(IMapper mapper, ILogger<OrderController> logger, IUserService userService)
            => (_mapper, _logger, _userService) = (mapper, logger, userService);

        /// <summary>
        /// Gets the list of orders
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET order/all
        /// </remarks>
        /// <returns>Returns AllOrdersVm</returns>
        /// <response code="200">Succes</response>
        /// <response code="401">User is unauthorized</response>
        [HttpGet("all")]
        [Cached(60)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<AllOrdersVm>> GetAll()
        {
            _logger.LogInformation("Here is my logger working");
            _logger.LogError(_userService.GetTraceIdentifier());
            _logger.LogError(_userService.GetUserName());

			var query = new GetAllOrdersQuery();

			var vm = await Mediator.Send(query);
			return Ok(vm);
        }


        /// <summary>
        /// Gets all exchanger's orders by Id of exchanger
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET order/allbyexchanger
        /// {
        ///     "ExchangerId":"c29af383-edfc-47cf-a50e-e55c3c6d19f5"
        /// }
        /// </remarks>
        /// <param name="orderListByExchangerDto">OrderListByExchangerDto object</param>
        /// <returns>Returns AllByExchangerVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="400">Bad request/validation failed</response>
		[HttpGet("allbyexchanger")]
        [Cached(60)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AllByExchangerVm>> GetAll(Guid Id)
        {
            var orderListByExchangerDto = new OrderListByExchangerDto { ExchangerId = Id };

            var query = _mapper.Map<AllByExchangerQuery>(orderListByExchangerDto);

			var vm = await Mediator.Send(query);

            if(vm.Error is not null)
            {
                _logger.LogError(vm.Error);
                _logger.LogInformation(_userService.GetUserName());
                _logger.LogTrace(_userService.GetTraceIdentifier());

                return BadRequest(vm);
            }

			return Ok(vm);
        }

        /// <summary>
        /// Create new order for exchanger
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST order/create
        /// {
        ///     "ExchangerId":"c29af383-edfc-47cf-a50e-e55c3c6d19f5",
        ///     "From":"SOL",
        ///     "To":"USDT,
        ///     "In":1.0,
        ///     "Out":343.23,
        ///     "Amount":2342.32,
        ///     "MinAmount":23.0,
        ///     "MaxAmount":2345.0
        /// }
        /// </remarks>
        /// <param name="createOrderDto">CreateOrderDtp object</param>
        /// <returns>Returns CreateOrderResultVm Object</returns>
        /// <response code="201">Success</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="400">Bad request/validation failed</response>
		[HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateOrderResultVm>> Create([FromBody] CreateOrderDto createOrderDto)
        {
			var command = _mapper.Map<CreateOrderCommand>(createOrderDto);

			var result = await Mediator.Send(command);

            if (result.Error is not null)
            {
                _logger.LogError(result.Error);
                _logger.LogTrace(_userService.GetTraceIdentifier());
                _logger.LogInformation(_userService.GetUserName());

                return BadRequest(result);
            }

			return Ok(result);
        }

        /// <summary>
        /// Gets list of orders by coins pair
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET orders/bycoins
        /// {
        ///     "From":"BTC",
        ///     "To":"XRP"
        /// }
        /// </remarks>
        /// <param name="coinFrom">String with coin name</param>
        /// <param name="coinTo">String with coin name</param>
        /// <returns>OrderListByCoinsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="400">Bad request/validation failed</response>
		[HttpGet("{coinFrom}-{coinTo}")]
		[Cached(600)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<OrderListByCoinsVm>> OrdersByCoins(string coinFrom, string coinTo)
        {
            var ordersByCoinsDto = new OrdersByCoinsDto { From = coinFrom, To = coinTo };

            var query = _mapper.Map<GetOrderListByCoinsQuery>(ordersByCoinsDto);

			var vm = await Mediator.Send(query);
			return Ok(vm);
        }

        /// <summary>
        /// Updates exchanger's order
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT orders/update
        /// {
        ///     "OrderId:"c29af383-edfc-47cf-a50e-e55c3c6d19f5",
        ///     "ExchangerId":"05bc4f0f-1979-48c3-80b0-ee992b67d19d",
        ///     "From":"BTC",
        ///     "To":"SHIBA,
        ///     "In":1.0,
        ///     "Out":343.23,
        ///     "Amount":2342.32,
        ///     "MinAmount":23.0,
        ///     "MaxAmount":2345.0
        /// </remarks>
        /// <param name="updateOrderDto">UpdateOrderDto object</param>
        /// <returns>Returns UpdateOrderResultVm Object</returns>
        /// <response code="204">Success</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="400">Bad request/validation failed</response>
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UpdateOrderResultVm>> Update([FromBody] UpdateOrderDto updateOrderDto)
        {
            var command = _mapper.Map<UpdateOrderCommand>(updateOrderDto);
            var result = await Mediator.Send(command);

            if(result.Error is not null)
            {
                _logger.LogError(result.Error);
                _logger.LogTrace(_userService.GetTraceIdentifier());
                _logger.LogInformation(_userService.GetUserName());

                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Delete exchanger's order
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE orders/delete/05bc4f0f-1979-48c3-80b0-ee992b67d19d/c29af383-edfc-47cf-a50e-e55c3c6d19f5
        /// <param name="exchangerId">Id of exchanger</param>
        /// <param name="orderId">Id of exhcanger's order</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="400">Bad request/validation failed</response>
        [HttpDelete("{exchangerId}/{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DeleteOrderResultVm>> Delete(Guid exchangerId, Guid orderId)
        {
            var deleteOrderDto = new DeleteOrderDto { ExchangerId = exchangerId, OrderId = orderId };

            var command = _mapper.Map<DeleteOrderCommand>(deleteOrderDto);

            var result = await Mediator.Send(command);

            if(result.Error is not null)
            {
                _logger.LogError(result.Error);
                _logger.LogTrace(_userService.GetTraceIdentifier());
                _logger.LogInformation(_userService.GetUserName());
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Create list of orders from XML file
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST order/createwithxml
        /// {
        ///     "ExchangerId":"05bc4f0f-1979-48c3-80b0-ee992b67d19d",
        ///     "FilePath":"/Users/SomeUser/Desktop/Orders.xml"
        /// }
        /// </remarks>
        /// <param name="createOrderListUsingXmlDto">CreateOrderListUsingXmlDto object</param>
        /// <returns>Returns OrderListFromXmlVm</returns>
        /// <response code="201">Success</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="400">Bad request/validation failed</response>
		[HttpPost("createwithxml")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderListFromXmlVm>> Create([FromBody]CreateOrderListUsingXmlDto createOrderListUsingXmlDto)
        {
			var query = _mapper.Map<CreateOrderListCommand>(createOrderListUsingXmlDto);

			var result = await Mediator.Send(query);

            if(result.Error is not null)
            {
                _logger.LogError(result.Error);
                _logger.LogTrace(_userService.GetTraceIdentifier());
                _logger.LogInformation(_userService.GetUserName());
                return BadRequest(result);
            }
			return Ok(result);
        }
    }
}

