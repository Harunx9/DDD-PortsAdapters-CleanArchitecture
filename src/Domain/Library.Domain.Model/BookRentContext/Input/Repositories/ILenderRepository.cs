using System.Threading.Tasks;
using Library.Domain.Model.BookRentContext.Input.Model;

namespace Library.Domain.Model.BookRentContext.Input.Repositories
{
    public interface ILenderRepository
    {
        Task<Lender> Get(LenderId id);
    }
}