using System;
using CleanArchitecture.Base.Validation.ConcreteValidators;

namespace CleanArchitecture.Base.Validation
{
    public static class DomainValidator
    {
        public static readonly StringValidator StringValidator = new StringValidator();

        public static void Valdate(Func<bool> predicate, string message)
        {
            if (predicate() == false)
                throw new DomainException(message);
        }

        public static void Fail(string message)
        {
            Valdate(() => false, message);
        }
    }
}
