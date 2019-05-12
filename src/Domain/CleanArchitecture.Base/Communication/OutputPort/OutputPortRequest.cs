using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CleanArchitecture.Base.Communication.OutputPort
{
    public class OutputPortRequest
    {
        public Guid RequestId { get; }
        private readonly IReadOnlyCollection<PropertyInfo> _properties;

        public OutputPortRequest()
        {
            RequestId = Guid.NewGuid();

            _properties = GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"{RequestId.ToString()} Output Request");

            foreach (var property in _properties)
            {
                builder.AppendLine($"Property {property.Name} = {property.GetValue(this)}");
            }

            return builder.ToString();
        }
    }
}