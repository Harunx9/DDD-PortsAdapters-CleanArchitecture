using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Base.Models;
using Library.Domain.Model.Common.ValueObjects;

namespace Library.Domain.Model.BookRentContext.Input.Model
{
    public class LenderId : IIdentity<Guid>
    {
        public Guid Id { get; }

        public LenderId(Guid id)
        {
            Id = id;
        }
    }

    public class Lender
    {
        public LenderId Id { get; }
        public FullName Name { get; }
        public ICollection<Book> RentedBooks { get; }

        private Lender(LenderId id, FullName name) : this()
        {
            Id = id;
            Name = name;
        }

        private Lender()
        {
            RentedBooks = Enumerable.Empty<Book>().ToList();
        }

        public static Lender Create(LenderId lenderId, FullName fullName)
        {
            return new Lender(lenderId, fullName);
        }
    }
}