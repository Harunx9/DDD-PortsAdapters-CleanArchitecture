using System;
using CleanArchitecture.Base.DependencyManagement;

namespace CleanArchitecture.Base.Wrappers
{
    public interface ITime
    {
        DateTime Now();
        DateTime NowUtc();
    }

    [Dependency(DependencyLifetime.SINGLETON)]
    public class Time : ITime
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }

        public DateTime NowUtc()
        {
            return DateTime.UtcNow;
        }
    }
}