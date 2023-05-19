namespace Bookstore.Domain.Models.Transformers;

public class FullNameFormatter : IAuthorNameTransformer
{
    public string Transform(Person person)
    {
        return $"{person.FirstName} {person.LastName}";
    }
}

public class SeperatedAuthorTransformer : IAuthorListTransformer
{
    private readonly string _separator;
    private readonly IAuthorNameTransformer _authorNameTransformer;

    public SeperatedAuthorTransformer(string separator, IAuthorNameTransformer authorNameTransformer)
    {
        _separator = separator;
        _authorNameTransformer = authorNameTransformer;
    }

    public string Transform(IEnumerable<Person> persons) =>
        string.Join(_separator, persons.Select(_authorNameTransformer.Transform));
}

public class AuthorAndTitlesTransformer : IBookTransformer
{
    private readonly IAuthorListTransformer _authorListTransformer;

    public AuthorAndTitlesTransformer(IAuthorListTransformer authorListTransformer)
    {
        _authorListTransformer = authorListTransformer;
    }

    public string Transform(Book book) =>
        $"{_authorListTransformer.Transform(book.Authors)} by {book.Title}";
}

public class TitleOnlyTransform : IBookTransformer
{
    public string Transform(Book book)
    {
        return book.Title;
    }
}