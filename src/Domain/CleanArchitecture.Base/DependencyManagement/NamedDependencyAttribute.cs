using System;

namespace CleanArchitecture.Base.DependencyManagement
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class NamedDependencyAttribute : DependencyAttribute
    {
        public string Name { get; }

        public NamedDependencyAttribute(DependencyLifetime lifetime, string name) : base(lifetime)
        {
            Name = name;
        }
    }
}