using System;
using CleanArchitecture.Base.Models;

namespace Library.Domain.Model.BookRentContext.Input.Model
{
    public sealed class BookId : IIdentity<Guid>
    {
        public Guid Id { get; }

        public BookId(Guid id)
        {
            Id = id;
        }
    }
}