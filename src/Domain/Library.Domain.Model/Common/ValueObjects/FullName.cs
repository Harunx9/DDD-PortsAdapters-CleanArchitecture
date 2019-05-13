using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Base.Models;
using CleanArchitecture.Base.Validation;

namespace Library.Domain.Model.Common.ValueObjects
{
    public class FullName : ValueObject
    {
        public string FirstName { get; }
        public string LastName { get; }

        public FullName(string firstName, string lastName)
        {
            DomainValidator.StringValidator.IsOnlyLetters(firstName, "First name should contains only letters");
            DomainValidator.StringValidator.IsOnlyLetters(firstName, "Last name should contains only letters");
            
            FirstName = firstName;
            LastName = lastName;
        }
        
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}