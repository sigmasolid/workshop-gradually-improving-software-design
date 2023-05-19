using Bookstore.Data.Seeding;
using Bookstore.Domain.Models;
using Bookstore.Domain.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Pages;

public class BooksModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly BookstoreDbContext _dbContext;
    public IEnumerable<Book> Books { get; private set; } = Enumerable.Empty<Book>();
    public IEnumerable<char> AuthorInitials { get; set; } = Enumerable.Empty<char>();
    private readonly IDataSeed<Book> _booksSeed;
    
    // private async Task PopulatePublishedAuthorInitials() =>
    //     this.AuthorInitials  = await _dbContext.BookAuthors
    //         .GetPublishedAuthors()
    //         .Select(author => author.LastName.Substring(0, 1))
    //         .Distinct()
    //         .OrderBy(initial => initial)
    //         .ToListAsync();

    public BooksModel(ILogger<IndexModel> logger, BookstoreDbContext dbContext, IDataSeed<Book> booksSeed)
    {
        _logger = logger;
        _dbContext = dbContext;
        _booksSeed = booksSeed;
    }

    public async Task OnGet([FromQuery] string? authorInitial)
    {
        await this._booksSeed.SeedAsync();
        this.Books = await _dbContext.Books.GetBooks()
            .FilterBookByAuthorInitial(authorInitial)
            .OrderBy(book => book.Title).ToListAsync();
        this.AuthorInitials = _dbContext.Books.GetAuthorInitials();
    }
}