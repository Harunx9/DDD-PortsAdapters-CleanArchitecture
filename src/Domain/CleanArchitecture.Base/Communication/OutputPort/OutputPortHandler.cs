using System.Threading.Tasks;

namespace CleanArchitecture.Base.Communication.OutputPort
{
    public interface IOutputPortHandler { }

    internal interface IOutputPortHandler<TOutput, TRequest> : IOutputPortHandler
    {
        Task<TOutput> Get(TRequest request);
    }
}