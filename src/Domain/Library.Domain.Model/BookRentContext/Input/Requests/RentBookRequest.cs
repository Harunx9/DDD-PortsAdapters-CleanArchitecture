using CleanArchitecture.Base.Communication.InputPort;
using Library.Domain.Model.BookRentContext.Input.Model;

namespace Library.Domain.Model.BookRentContext.Input.Requests
{
    public sealed class RentBookRequest : InputRequest
    {
        public BookId BookId { get; }
        public LenderId LenderId { get; }

        public RentBookRequest(BookId bookId, LenderId lenderId)
        {
            BookId = bookId;
            LenderId = lenderId;
        }
    }
}