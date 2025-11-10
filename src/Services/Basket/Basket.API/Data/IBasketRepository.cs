namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string Username, CancellationToken cancellationToken = default);

    Task<ShoppingCart> StoreBasket(ShoppingCart Cart, CancellationToken cancellationToken = default);

    Task<bool> DeleteBasket(string Username, CancellationToken cancellationToken = default);
}
