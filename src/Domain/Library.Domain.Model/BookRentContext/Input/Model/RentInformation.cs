using System;
using CleanArchitecture.Base.Models;

namespace Library.Domain.Model.BookRentContext.Input.Model
{
    public class RentInformation : IIdentity<int>
    {
        public int Id { get; private set; }
        public Book Book { get; private set; }
        public Lender Lender { get; private set; }
        public DateTime CreateDate { get; private set; }

        public RentInformation(Book book, DateTime rentTime, Lender lender)
        {
            Book = book;
            CreateDate = rentTime;
            Lender = lender;
        }
    }
}