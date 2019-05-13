using System;
using CleanArchitecture.Base.Wrappers;
using Moq;

namespace Test.UT
{
    public abstract class unit_test_case : IDisposable
    {
        protected Mock<ITime> _timeMock;
        protected DateTime _now;

        private bool _disposedValue = false;

        public unit_test_case()
        {
            _now = DateTime.Now;
            _timeMock = new Mock<ITime>();

            // ReSharper disable once VirtualMemberCallInConstructor
            Arrange();
            // ReSharper disable once VirtualMemberCallInConstructor
            Act();
        }

        protected abstract void Arrange();

        protected abstract void Act();

        protected virtual void Cleanup()
        {
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue != false) return;

            if (disposing)
            {
                Cleanup();
            }

            _disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}