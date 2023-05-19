namespace Bookstore.Domain.Models.Transformers;

public interface IBookTransformer
{
    string Transform(Book book);
}