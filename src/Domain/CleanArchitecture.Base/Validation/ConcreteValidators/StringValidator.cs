using System;
using System.Linq;

namespace CleanArchitecture.Base.Validation.ConcreteValidators
{
    public class StringValidator
    {
        public void NotEmpty(string value, string fieldName)
        {
            DomainValidator.Valdate(() => string.IsNullOrWhiteSpace(value) == true, $"{fieldName} should not be empty");
        }

        public void IsOnlyLetters(string input, string message)
        {
            DomainValidator.Valdate(() => input.All(Char.IsLetter), message);
        }
    }
}
