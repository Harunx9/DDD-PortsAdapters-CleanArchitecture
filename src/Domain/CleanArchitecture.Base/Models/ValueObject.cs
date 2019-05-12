using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CleanArchitecture.Base.Models
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        private IReadOnlyCollection<FieldInfo> _fields;
        private IReadOnlyCollection<PropertyInfo> _properies;

        public static bool operator==(ValueObject left, ValueObject right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return left.Equals(right) == false;
        }

        public bool Equals(ValueObject other)
        {
            return Equals(other as object);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            return GetProperties().All(property => object.Equals(property.GetValue(this), property.GetValue(obj))) &&
                GetFields().All(field => object.Equals(field.GetValue(this), field.GetValue(obj)));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var list = new List<int> { };

                list.AddRange(GetFields().Select(GetHashFromField));
                list.AddRange(GetProperties().Select(GetHashFromProperty));

                return list.Aggregate((x, y) => x ^ y);

                int GetHashFromField(FieldInfo info)
                {
                    var value = info.GetValue(this);
                    return value != null ? value.GetHashCode() : 0;
                }

                int GetHashFromProperty(PropertyInfo info)
                {
                    var value = info.GetValue(this);
                    return value != null ? value.GetHashCode() : 0;
                }
            }
        }

        private IReadOnlyCollection<PropertyInfo> GetProperties()
        {
            if (_properies == null)
            {
                _properies = GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public);
            }

            return _properies
                   ?? Enumerable.Empty<PropertyInfo>()
                   .ToList().AsReadOnly();
        }

        private IReadOnlyCollection<FieldInfo> GetFields()
        {
            if (_fields == null)
            {
                _fields = GetType()
                    .GetFields(BindingFlags.Instance | BindingFlags.Public);
            }

            return _fields
                   ?? Enumerable.Empty<FieldInfo>()
                   .ToList().AsReadOnly();
        }
    }
}
