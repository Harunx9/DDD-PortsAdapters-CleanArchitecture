using System;
namespace CleanArchitecture.Base.Exceptions
{
    [System.Serializable]
    public class ArchitectureVolationException : Exception
    {
        public ArchitectureVolationException()
        {
        }

        public ArchitectureVolationException(string message) : base(message)
        {
        }

        public ArchitectureVolationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ArchitectureVolationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
