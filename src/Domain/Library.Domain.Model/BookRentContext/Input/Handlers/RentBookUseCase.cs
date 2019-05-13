using System.Threading.Tasks;
using CleanArchitecture.Base.Communication.InputPort;
using CleanArchitecture.Base.DependencyManagement;
using CleanArchitecture.Base.Wrappers;
using Library.Domain.Model.BookRentContext.Input.Repositories;
using Library.Domain.Model.BookRentContext.Input.Requests;

namespace Library.Domain.Model.BookRentContext.Input.Handlers
{
    
    [Dependency(DependencyLifetime.NEW_PER_EXECUTION)]
    public  class RentBookUseCase : IInputPortHandler<RentBookRequest>
    {
        private readonly IBookForRentRepository _bookForRentRepository;
        private readonly ILenderRepository _lenderRepository;
        private readonly ITime _time;

        public RentBookUseCase(IBookForRentRepository bookForRentRepository, ITime time, ILenderRepository lenderRepository)
        {
            _bookForRentRepository = bookForRentRepository;
            _time = time;
            _lenderRepository = lenderRepository;
        }

        public async Task Handle(RentBookRequest request)
        {
            //Todo: add lender not returned book validation
            
            var bookForRent = await _bookForRentRepository.Get(request.BookId);

            var lender = await _lenderRepository.Get(request.LenderId);
            
            bookForRent.RentBook(lender, _time.Now());
            
            await _bookForRentRepository.Save();
        }
    }
}