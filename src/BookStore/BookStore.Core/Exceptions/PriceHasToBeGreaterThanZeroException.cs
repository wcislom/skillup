using Shared.Core;

namespace BookStore.Core.Exceptions;

[Serializable]
internal class PriceHasToBeGreaterThanZeroException : DomainException
{
    public decimal Price { get; }

    public PriceHasToBeGreaterThanZeroException(decimal price)
        : base($"Price has to be greater than zero. Current price: {price}")
    {
        Price = price;
    }
}