using CleanArchitecture.Base.Communication.OutputPort;

namespace Test.UT.CleanArchitectureBaseTests.Communication.OutputPort
{
    public sealed class TestOneOutput
    {
        public string Test => "Test1";
    }

    public sealed class TestTwoOutput
    {
        public string Test => "Test2";
    }

    public sealed class TestOneRequest : OutputPortRequest
    {
    }

    public sealed class TestTwoRequest : OutputPortRequest
    {
    }
}