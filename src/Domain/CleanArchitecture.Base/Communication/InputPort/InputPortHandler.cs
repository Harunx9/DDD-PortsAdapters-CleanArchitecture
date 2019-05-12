using System.Runtime.CompilerServices;
using System.Threading.Tasks;
namespace CleanArchitecture.Base.Communication.InputPort
{
    public interface IInputPortHandler
    {
    }

    internal interface IInputPortHandler<in TRequest> : IInputPortHandler
        where TRequest : InputRequest
    {
        Task Handle(TRequest request);
    }
}