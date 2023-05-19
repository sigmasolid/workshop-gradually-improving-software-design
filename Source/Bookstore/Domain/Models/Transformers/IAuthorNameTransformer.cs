namespace Bookstore.Domain.Models.Transformers;

public interface IAuthorNameTransformer
{
    string Transform(Person person);
}