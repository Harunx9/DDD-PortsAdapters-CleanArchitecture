using System;

namespace CleanArchitecture.Base.DependencyManagement
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DependencyAttribute : Attribute
    {
        public DependencyLifetime Lifetime { get; }

        public DependencyAttribute(DependencyLifetime lifetime)
        {
            Lifetime = lifetime;
        }
    }
}