namespace Doch.Models.Exceptions
{

    [Serializable]
    public class DataConflictException : DataValidationException
    {
        public DataConflictException() { }
        public DataConflictException(string message) : base(message) { }
        public DataConflictException(string fieldName, string message) : base(fieldName, message) { }
        public DataConflictException(string message, Exception inner) : base(message, inner) { }
        public DataConflictException(string fieldName, string message, Exception inner) : base(fieldName, message, inner) { }
        protected DataConflictException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
