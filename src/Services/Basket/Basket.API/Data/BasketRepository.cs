namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<bool> DeleteBasket(string Username, CancellationToken cancellationToken = default)
    {
        session.Delete<ShoppingCart>(Username);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<ShoppingCart> GetBasket(string Username, CancellationToken cancellationToken = default)
    {
        var basket = await session.LoadAsync<ShoppingCart>(Username, cancellationToken) ??
            throw new BasketNotFoundException(Username);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart Cart, CancellationToken cancellationToken = default)
    {
        session.Store(Cart);
        await session.SaveChangesAsync(cancellationToken);
        return Cart;
    }
}
