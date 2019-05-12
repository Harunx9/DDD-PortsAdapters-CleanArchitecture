using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CleanArchitecture.Base.Exceptions;

namespace CleanArchitecture.Base.Communication.OutputPort
{
    public interface IOutputPortDispatcher
    {
        Task<TOutput> Dispatch<TOutput, TRequest>(TRequest request) where TRequest : OutputPortRequest;
    }

    public class OutputPortDispatcher : IOutputPortDispatcher
    {
        private const string GET_FUNCTION_NAME = "Get";
        
        private readonly ConcurrentDictionary<ValueTuple<Type, Type>, ExecutionContext> _handlers;

        public OutputPortDispatcher(IEnumerable<IOutputPortHandler> handlers)
        {
            _handlers = new ConcurrentDictionary<ValueTuple<Type, Type>, ExecutionContext>(handlers
                .Select(HandleMethodSelector)
                .ToDictionary(k => k.key, v => v.ctx));
        }

        private ((Type responseType, Type requestType) key, ExecutionContext ctx) HandleMethodSelector(
            IOutputPortHandler handler)
        {
            var type = handler.GetType();

            var methodInfo = type.GetMethod(GET_FUNCTION_NAME);

            var @interface = type.GetInterfaces()
                .FirstOrDefault(x => x.IsGenericType && typeof(IOutputPortHandler<,>) == x.GetGenericTypeDefinition());

            if(methodInfo == null || @interface == null)
                throw new ArchitectureVolationException(
                    $"Every {nameof(IOutputPortHandler)} should implement generic interface with \"Get\" method");
            
            var responseType = @interface?.GetGenericArguments().FirstOrDefault();
            
            var requestType = @interface?.GetGenericArguments().LastOrDefault();

            return ((responseType, requestType), new ExecutionContext(methodInfo, handler));
        }


        public Task<TOutput> Dispatch<TOutput, TRequest>(TRequest request) where TRequest : OutputPortRequest
        {
            return _handlers[(typeof(TOutput), typeof(TRequest))].Call<TOutput>(request);
        }
        
        private class ExecutionContext
        {
            private readonly MethodInfo _method;
            private readonly IOutputPortHandler _executionObject;

            public ExecutionContext(MethodInfo method, IOutputPortHandler executionObject)
            {
                _method = method;
                _executionObject = executionObject;
            }

            public Task<TOutput> Call<TOutput>(params object[] patameters)
            {
                return _method.Invoke(_executionObject, patameters) as Task<TOutput>;
            }
        }
    }
}