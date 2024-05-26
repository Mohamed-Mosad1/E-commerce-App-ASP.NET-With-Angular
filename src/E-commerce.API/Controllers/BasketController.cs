using AutoMapper;
using E_commerce.Core.Dtos;
using E_commerce.Core.Entities.Basket;
using E_commerce.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket(string basketId)
        {
            var basket = await _basketRepository.GetBasketByIdAsync(basketId);
            return Ok(basket ?? new CustomerBasket(basketId));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket(CustomerBasketDto customerBasket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(customerBasket);

            var basket = await _basketRepository.UpdateBasketAsync(mappedBasket);

            return Ok(basket);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string basketId)
        {
            return Ok(await _basketRepository.DeleteBasketAsync(basketId));
        }


    }
}
