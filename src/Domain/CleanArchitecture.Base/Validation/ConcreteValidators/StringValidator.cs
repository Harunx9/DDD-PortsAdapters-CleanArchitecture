namespace CleanArchitecture.Base.Validation.ConcreteValidators
{
    public class StringValidator
    {
        public void NotEmpty(string value, string fieldName)
        {
            DomainValidator.Valdate(() => string.IsNullOrWhiteSpace(value) == true, $"{fieldName} should not be empty");
        }
    }
}
