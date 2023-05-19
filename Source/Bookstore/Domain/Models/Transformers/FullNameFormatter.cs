namespace Bookstore.Domain.Models.Transformers;

public class FullNameFormatter : IAuthorNameTransformer
{
    public string Transform(Person person)
    {
        return $"{person.FirstName} {person.LastName}";
    }
}