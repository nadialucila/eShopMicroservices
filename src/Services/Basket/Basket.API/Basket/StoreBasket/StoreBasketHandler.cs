namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string Username);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart should not be null.");
        RuleFor(x => x.Cart.Username).NotEmpty().WithMessage("Username is required.");
    }
}

public class StoreBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart Cart = command.Cart;

        await basketRepository.StoreBasket(Cart, cancellationToken);
        //update cache

        return new StoreBasketResult(command.Cart.Username);
    }
}
