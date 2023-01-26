namespace Doch.Models.Exceptions
{

    [Serializable]
    public class DataValidationException : DataException
    {
        public DataValidationException() { }
        public DataValidationException(string message) : base(message) { }
        public DataValidationException(string message, Exception inner) : base(message, inner) { }
        public DataValidationException(string fieldName, string message) : base(message)
        {
            FieldName = fieldName;
        }
        public DataValidationException(string fieldName, string message, Exception inner) : base(message, inner)
        {
            FieldName = fieldName;
        }
        protected DataValidationException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public string FieldName { get; set; } = string.Empty;
    }
}
