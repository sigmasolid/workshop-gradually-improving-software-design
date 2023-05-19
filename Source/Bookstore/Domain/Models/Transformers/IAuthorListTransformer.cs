namespace Bookstore.Domain.Models.Transformers;

public interface IAuthorListTransformer
{
    string Transform(IEnumerable<string> names);
}