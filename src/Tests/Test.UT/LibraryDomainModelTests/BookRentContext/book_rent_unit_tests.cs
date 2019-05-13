using System;
using System.Threading.Tasks;
using CleanArchitecture.Base.Communication.InputPort;
using FluentAssertions;
using Library.Domain.Model.BookRentContext.Input.Handlers;
using Library.Domain.Model.BookRentContext.Input.Model;
using Library.Domain.Model.BookRentContext.Input.Repositories;
using Library.Domain.Model.BookRentContext.Input.Requests;
using Library.Domain.Model.Common.ValueObjects;
using Moq;
using Test.UT.Base;

namespace Test.UT.LibraryDomainModelTests.BookRentContext
{
    public sealed class when_rent_book_without_any_queue : unit_test_case
    {
        private BookId _bookId = new BookId(Guid.NewGuid());
        private LenderId _lenderId = new LenderId(Guid.NewGuid());

        private Book _book;
        private Lender _lender;

        private Mock<IBookForRentRepository> _bookForRentRepositoryMock;
        private Mock<ILenderRepository> _lenderRepositoryMock;

        private IInputPortHandler<RentBookRequest> _handler;

        protected override void Arrange()
        {
            _book = Book.Create(_bookId);
            _lender = Lender.Create(_lenderId, new FullName("Tony", "Stark"));

            _timeMock.Setup(x => x.Now()).Returns(_now);

            _bookForRentRepositoryMock = new Mock<IBookForRentRepository>();
            _bookForRentRepositoryMock.Setup(x => x.Get(It.IsAny<BookId>())).Returns(Task.Run(() => _book));
            
            _lenderRepositoryMock = new Mock<ILenderRepository>();
            _lenderRepositoryMock.Setup(x => x.Get(It.IsAny<LenderId>())).Returns(Task.Run(() => _lender));

            _handler = new RentBookUseCase(_bookForRentRepositoryMock.Object, _timeMock.Object, _lenderRepositoryMock.Object);
        }

        protected override void Act() => _handler.Handle(new RentBookRequest(_bookId, _lenderId))
            .GetAwaiter()
            .GetResult();

        [Assert]
        public void then_new_rent_should_be_added()
            => _book.GetLastRentInformation().Should().NotBeNull();

        [Assert]
        public void then_lender_name_should_be_Tony_Stark()
            => _book.GetLastRentInformation().Lender.Name.ToString().Should().Be("Tony Stark");
    }
}