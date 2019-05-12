using System;

namespace Test.UT
{
    public abstract class unit_test_case : IDisposable
    {
        private bool _disposedValue = false;

        public unit_test_case()
        {
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