using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CleanArchitecture.Base.DependencyManagement;
using CleanArchitecture.Base.Exceptions;

namespace CleanArchitecture.Base.Communication.InputPort
{
    public interface IInputPortDispatcher
    {
        Task Dispatch<TRequest>(TRequest request) where TRequest : InputRequest;
    }

    [Dependency(DependencyLifetime.SINGLETON)]
    public class InputPortDispatcher : IInputPortDispatcher
    {
        private const string HANDLER_METHOD_NAME = "Handle";

        private readonly ConcurrentDictionary<Type, ExecutionContext> _handers;

        public InputPortDispatcher(IEnumerable<IInputPortHandler> inputPortHandlers)
        {
            _handers = new ConcurrentDictionary<Type, ExecutionContext>(
                inputPortHandlers.Select(HandlerMethodSelector)
                    .ToDictionary(x => x.requestType, x => x.ctx));
        }

        public Task Dispatch<TRequest>(TRequest request) where TRequest : InputRequest
        {
            return _handers[typeof(TRequest)].Call(request);
        }

        private static (Type requestType, ExecutionContext ctx) HandlerMethodSelector(
            IInputPortHandler portHandler)
        {
            var type = portHandler.GetType();

            var methodInfo = type.GetMethod(HANDLER_METHOD_NAME);
            var @interface = type.GetInterfaces()
                .FirstOrDefault(x => x.IsGenericType && typeof(IInputPortHandler<>) == x.GetGenericTypeDefinition());
            var genericParameter = @interface?.GetGenericArguments()
                .FirstOrDefault();

            if (methodInfo == null || genericParameter == null)
                throw new ArchitectureVolationException(
                    $"Every {nameof(IInputPortHandler)} should implement generic interface with \"Handle\" method");

            return (genericParameter, new ExecutionContext(methodInfo, portHandler));
        }
        
        private class ExecutionContext
        {
            private readonly MethodInfo _method;
            private readonly IInputPortHandler _executionObject;

            public ExecutionContext(MethodInfo method, IInputPortHandler executionObject)
            {
                _method = method;
                _executionObject = executionObject;
            }

            public Task Call(params object[] patameters)
            {
                return _method.Invoke(_executionObject, patameters) as Task;
            }
        }
    }
}