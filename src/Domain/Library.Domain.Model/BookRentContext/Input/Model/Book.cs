using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Domain.Model.BookRentContext.Input.Model
{
    public sealed class Book
    {
        public BookId Id { get; private set; }
        public ICollection<RentInformation> RentInformations { get; private set; }

        public static Book Create(BookId id)
        {
            return new Book(id);
        }

        private Book(BookId id) : this()
        {
            Id = id;
        }

        private Book()
        {
            RentInformations = new List<RentInformation>();
        }

        public RentInformation GetLastRentInformation()
        {
            return RentInformations.OrderBy(x => x.CreateDate).Last();
        }

        public void RentBook(Lender lender, DateTime rentTime)
        {
            RentInformations.Add(new RentInformation(this, rentTime, lender));
        }
    }
}