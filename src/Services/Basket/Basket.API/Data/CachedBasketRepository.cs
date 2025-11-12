namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository basketRepository, 
    IDistributedCache cache) 
    : IBasketRepository
{
    public async Task<bool> DeleteBasket(string Username, CancellationToken cancellationToken = default)
    {

        await basketRepository.DeleteBasket(Username, cancellationToken);
        await cache.RemoveAsync(Username, cancellationToken);

        return true;
    }

    public async Task<ShoppingCart> GetBasket(string Username, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(Username, cancellationToken);

        if(!string.IsNullOrEmpty(cachedBasket))
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
        
        var basket = await basketRepository.GetBasket(Username, cancellationToken);
        await cache.SetStringAsync(Username, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart Cart, CancellationToken cancellationToken = default)
    {
        await basketRepository.StoreBasket(Cart, cancellationToken);
        await cache.SetStringAsync(Cart.Username, JsonSerializer.Serialize(Cart), cancellationToken);

        return Cart;
    }
}
