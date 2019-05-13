using System.Threading.Tasks;
using Library.Domain.Model.BookRentContext.Input.Model;

namespace Library.Domain.Model.BookRentContext.Input.Repositories
{
    public interface IBookForRentRepository
    {
        Task<Book> Get(BookId bookId);

        Task Save();
    }
}