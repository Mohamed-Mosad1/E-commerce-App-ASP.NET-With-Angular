using E_commerce.Core.Entities.Basket;

namespace E_commerce.Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketByIdAsync(string basketId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basketDto);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
