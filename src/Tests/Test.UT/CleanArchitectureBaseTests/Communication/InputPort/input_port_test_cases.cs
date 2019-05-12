// ReSharper disable InconsistentNaming

using System;
using CleanArchitecture.Base.Communication.InputPort;
using CleanArchitecture.Base.Exceptions;
using FluentAssertions;
using Moq;
using Test.UT.Base;
using Xunit;

namespace Test.UT.CleanArchitectureBaseTests.Communication.InputPort
{
    public class execution_incorrect_implementation_handler : unit_test_case
    {
        private Mock<IInputPortHandler> _handlerMock;
        private Exception _result;

        protected override void Arrange()
        {
            _handlerMock = new Mock<IInputPortHandler>(MockBehavior.Loose);
        }

        protected override void Act() =>
            _result = Record.Exception(() => new InputPortDispatcher(new[] {_handlerMock.Object}));

        [Assert]
        public void exception_should_be_thrown()
        {
            _result.Should().BeOfType<ArchitectureVolationException>();
            _result.Message.Should()
                .Contain("Every IInputPortHandler should implement generic interface with \"Handle\" method");
        }
    }

    public class execution_non_dependency_input_port : unit_test_case
    {
        private Mock<IInputPortHandler<TestOneRequest>> _handlerMock;
        private IInputPortDispatcher _dispatcher;

        protected override void Arrange()
        {
            _handlerMock = new Mock<IInputPortHandler<TestOneRequest>>(MockBehavior.Loose);

            _dispatcher = new InputPortDispatcher(new IInputPortHandler[] {_handlerMock.Object});
        }

        protected override void Act() => _dispatcher.Dispatch(new TestOneRequest())
            .GetAwaiter()
            .GetResult();

        [Assert]
        public void handler_mock_should_be_called()
            => _handlerMock.Verify(x => x.Handle(It.IsAny<TestOneRequest>()), Times.Once);
    }

    public class execution_multiple_handlers : unit_test_case
    {
        private Mock<IInputPortHandler<TestOneRequest>> _handlerOneMock;
        private Mock<IInputPortHandler<TestTwoRequest>> _handlerTwoMock;
        private IInputPortDispatcher _dispatcher;

        protected override void Arrange()
        {
            _handlerOneMock = new Mock<IInputPortHandler<TestOneRequest>>(MockBehavior.Loose);
            _handlerTwoMock = new Mock<IInputPortHandler<TestTwoRequest>>(MockBehavior.Loose);

            _dispatcher = new InputPortDispatcher(new IInputPortHandler[]
                {_handlerOneMock.Object, _handlerTwoMock.Object});
        }

        protected override void Act() => _dispatcher.Dispatch(new TestTwoRequest()).GetAwaiter().GetResult();

        [Assert]
        public void correct_handler_should_be_called()
        {
            _handlerOneMock.Verify(x => x.Handle(It.IsAny<TestOneRequest>()), Times.Never);
            _handlerTwoMock.Verify(x => x.Handle(It.IsAny<TestTwoRequest>()), Times.Once);
        }
    }
}