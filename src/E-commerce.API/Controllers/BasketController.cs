using E_commerce.Core.Entities;
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

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            return Ok(basket ?? new CustomerBasket(basketId));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket(CustomerBasket customerBasket)
        {
            var basket = await _basketRepository.UpdateBasketAsync(customerBasket);

            return Ok(basket);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string basketId)
        {
            return Ok(await _basketRepository.DeleteBasketAsync(basketId));
        }


    }
}
