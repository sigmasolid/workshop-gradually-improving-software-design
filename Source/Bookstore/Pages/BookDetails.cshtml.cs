using Bookstore.Domain.Common;
using Bookstore.Domain.Models;
using Bookstore.Domain.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookstore.Pages;

public class BookDetailsModel : PageModel
{
    public record PriceLine(string Label, Money Amount);

    private readonly ILogger<IndexModel> _logger;
    private readonly BookstoreDbContext _dbContext;
    private readonly Discounts _discounts;
    public Book Book { get; private set; } = null!;

    public IReadOnlyList<PriceLine> PriceSpecification { get; private set; } = Array.Empty<PriceLine>();

    public BookDetailsModel(ILogger<IndexModel> logger, BookstoreDbContext dbContext, Discounts discounts)
    {
        _discounts = discounts;
        (_logger, _dbContext) = (logger, dbContext);
    }

    public async Task<IActionResult> OnGet(Guid id)
    {
        if ((await _dbContext.Books.GetBooks().ById(id)) is Book book)
        {
            this.Book = book;
            var price = BookPricing.SeedPriceFor(book, Currency.USD).Value;
            if (_discounts.RelativeDiscount > 0)
            {
                var discount = price * _discounts.RelativeDiscount;
                var discountedPrice = price * (1 - _discounts.RelativeDiscount);
                this.PriceSpecification = new List<PriceLine>()
                {
                    new("Price", price),
                    new($"Discount {_discounts.RelativeDiscount*100}%", discount),
                    new("Discounted Price", discountedPrice)
                };
            }
            else
            {
                this.PriceSpecification = new List<PriceLine>()
                    { new("Price", price) };
            }

            return Page();
        }

        return Redirect("/books");
    }
}