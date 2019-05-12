using System;
using System.Threading.Tasks;
using CleanArchitecture.Base.Communication.OutputPort;
using CleanArchitecture.Base.Exceptions;
using FluentAssertions;
using Moq;
using Test.UT.Base;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Test.UT.CleanArchitectureBaseTests.Communication.OutputPort
{
    public class execute_not_correct_implemented_interface : unit_test_case
    {
        private Mock<IOutputPortHandler> _handlerMock;
        private Exception _result;

        protected override void Arrange()
        {
            _handlerMock = new Mock<IOutputPortHandler>();
        }

        protected override void Act() => _result =
            Record.Exception(() => new OutputPortDispatcher(new IOutputPortHandler[] {_handlerMock.Object}));

        [Assert]
        public void exception_should_been_thrown()
        {
            _result.Should().BeOfType<ArchitectureVolationException>();
            _result.Message.Should()
                .Contain("Every IOutputPortHandler should implement generic interface with \"Get\" method");
        }
    }

    public class execute_dispatcher_with_one_handler_with_correct_parameters : unit_test_case
    {
        private IOutputPortDispatcher _dispatcher;
        private Mock<IOutputPortHandler<TestOneOutput, TestOneRequest>> _handlerMock;
        private TestOneOutput _result;

        protected override void Arrange()
        {
            _handlerMock = new Mock<IOutputPortHandler<TestOneOutput, TestOneRequest>>();
            _handlerMock.Setup(x => x.Get(It.IsAny<TestOneRequest>()))
                .Returns(() => Task.Factory.StartNew(() => new TestOneOutput()));

            _dispatcher = new OutputPortDispatcher(new IOutputPortHandler[] {_handlerMock.Object});
        }

        protected override void Act() =>
            _result = _dispatcher.Dispatch<TestOneOutput, TestOneRequest>(new TestOneRequest()).GetAwaiter()
                .GetResult();

        [Assert]
        public void result_should_not_be_null()
            => _result.Should().NotBeNull();

        [Assert]
        public void result_should_have_correct_value()
            => _result.Test.Should().Contain("Test1");
    }

    public class execute_dispatcher_with_many_handlers_with_correct_parameters : unit_test_case
    {
        private IOutputPortDispatcher _dispatcher;
        private Mock<IOutputPortHandler<TestOneOutput, TestOneRequest>> _handlerOneMock;
        private Mock<IOutputPortHandler<TestTwoOutput, TestTwoRequest>> _handlerTwoMock;
        private TestTwoOutput _result;

        protected override void Arrange()
        {
            _handlerOneMock = new Mock<IOutputPortHandler<TestOneOutput, TestOneRequest>>();
            _handlerOneMock.Setup(x => x.Get(It.IsAny<TestOneRequest>()))
                .Returns(() => Task.Factory.StartNew(() => new TestOneOutput()));

            _handlerTwoMock = new Mock<IOutputPortHandler<TestTwoOutput, TestTwoRequest>>();
            _handlerTwoMock.Setup(x => x.Get(It.IsAny<TestTwoRequest>()))
                .Returns(() => Task.Factory.StartNew(() => new TestTwoOutput()));

            _dispatcher = new OutputPortDispatcher(new IOutputPortHandler[]
                {_handlerOneMock.Object, _handlerTwoMock.Object});
        }

        protected override void Act() =>
            _result = _dispatcher.Dispatch<TestTwoOutput, TestTwoRequest>(new TestTwoRequest()).GetAwaiter()
                .GetResult();

        [Assert]
        public void result_should_not_be_null()
            => _result.Should().NotBeNull();

        [Assert]
        public void result_should_have_correct_value()
            => _result.Test.Should().Contain("Test2");
    }
}