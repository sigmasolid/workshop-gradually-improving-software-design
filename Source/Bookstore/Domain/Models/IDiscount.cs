using Bookstore.Domain.Common;

namespace Bookstore.Domain.Models;

public interface IDiscount
{
    IEnumerable<DiscountApplication> GetDiscountApplicationFor(Money price);
}

public record DiscountApplication(string Label, Money Amount);

public class NoDiscount : IDiscount
{
    public IEnumerable<DiscountApplication> GetDiscountApplicationFor(Money price) => Array.Empty<DiscountApplication>();
}

public class RelativeDiscount : IDiscount
{
    private readonly decimal _relativeDiscount;

    public RelativeDiscount(decimal relativeDiscount)
    {
        if (relativeDiscount <= 0 || relativeDiscount > 1)
            throw new ArgumentOutOfRangeException(nameof(relativeDiscount),
                "Relative discount must be between higher than 0 and equal or smaller than 1");
        _relativeDiscount = relativeDiscount;
    }

    public IEnumerable<DiscountApplication> GetDiscountApplicationFor(Money price)
    {
        var discount = price * _relativeDiscount;
        return new List<DiscountApplication>()
        {
            new($"Discount {_relativeDiscount * 100}%", discount)
        };
    }
}