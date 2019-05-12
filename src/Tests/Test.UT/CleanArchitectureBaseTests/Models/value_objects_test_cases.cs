using CleanArchitecture.Base.Models;
using FluentAssertions;
using Test.UT.Base;

namespace Test.UT.CleanArchitectureBaseTests.Models
{
    public class compare_two_value_objects_with_same_values : unit_test_case
    {
        private TestValueObject _left;
        private TestValueObject _right;
        private bool _result;


        protected override void Arrange()
        {
            _left = new TestValueObject("Test", 1);
            _right = new TestValueObject("Test", 1);
        }

        protected override void Act() => _result = _left == _right;

        [Assert]
        public void objects_should_be_equal()
            => _result.Should().BeTrue();
    }

    public class compare_two_value_objects_with_different_values : unit_test_case
    {
        private TestValueObject _left;
        private TestValueObject _right;
        private bool _result;


        protected override void Arrange()
        {
            _left = new TestValueObject("Test", 1);
            _right = new TestValueObject("TTest", 2);
        }

        protected override void Act() => _result = _left == _right;

        [Assert]
        public void objects_should_not_be_equal()
            => _result.Should().BeFalse();
    }

    public class TestValueObject : ValueObject
    {
        public string String { get; }
        public int Int { get; }

        public TestValueObject(string str, int intt)
        {
            String = str;
            Int = intt;
        }
    }
}