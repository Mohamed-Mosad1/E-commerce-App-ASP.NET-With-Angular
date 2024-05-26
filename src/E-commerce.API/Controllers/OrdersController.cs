using AutoMapper;
using E_commerce.API.Errors;
using E_commerce.Core.Dtos;
using E_commerce.Core.Entities.OrderAggregate;
using E_commerce.Core.Interfaces;
using E_commerce.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IUnitOfWork unitOfWork, IOrderService orderService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost("create-order")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var OrderAddress = _mapper.Map<AddressDto, ShippingAddress>(orderDto.ShippingAddress);

            var order = await _orderService.CreateOrderAsync(buyerEmail, orderDto.DeliveryMethodId, orderDto.BasketId, OrderAddress);

            if (order is null) return BadRequest(new BaseCommonResponse(400, "Error While Creating Order"));
           
            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }


        [HttpGet("get-orders-for-user")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrdersForUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var orders = await _orderService.GetOrdersForUserAsync(email);

            var mappedOrders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders);

            return Ok(mappedOrders);
        }

        [HttpGet("get-order-by-id/{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetOrderByIdAsync(id, email);
            if (order is null) return NotFound(new BaseCommonResponse(404));

            var mappedOrder = _mapper.Map<Order, OrderToReturnDto>(order);

            return Ok(mappedOrder);
        }

        [HttpGet("get-delivery-methods")]
        public async Task<ActionResult<Order>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }

    }
}
