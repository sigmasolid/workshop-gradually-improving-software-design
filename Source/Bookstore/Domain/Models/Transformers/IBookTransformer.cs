namespace Bookstore.Domain.Models.Transformers;

public interface IBookTransformer
{
    string Transform(Book book);
}

public class FullNameFormatter : IAuthorNameTransformer
{
    public string Transform(Person person)
    {
        return $"{person.FirstName} {person.LastName}";
    }
}