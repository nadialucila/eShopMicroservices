namespace Basket.API.Basket.GetBasket;

public record GetBasketRequest();

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket", async (ISender sender) =>
        {

        });
    }
}
