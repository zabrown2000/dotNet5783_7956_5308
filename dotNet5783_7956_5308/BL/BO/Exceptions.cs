namespace BO;

    public class BOEntityDoesNotExistException : Exception
    {
        public BOEntityDoesNotExistException() { }
        public BOEntityDoesNotExistException(String message) : base(message) { }
        public BOEntityDoesNotExistException(String message, Exception inner) : base(message, inner) { }
        protected BOEntityDoesNotExistException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public class InvalidInputException : Exception
    {
        public InvalidInputException() { }
        public InvalidInputException(string message) : base(message) { }
        public InvalidInputException(string message, Exception inner) : base(message, inner) { }
        protected InvalidInputException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public class BOEntityAlreadyExistsException : Exception
    {
        public BOEntityAlreadyExistsException() { }
        public BOEntityAlreadyExistsException(string message) : base(message) { }
        public BOEntityAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
        protected BOEntityAlreadyExistsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public class OutOfStockException : Exception
    {
        public OutOfStockException() { }
        public OutOfStockException(string message) : base(message) { }
        public OutOfStockException(string message, Exception inner) : base(message, inner) { }
        protected OutOfStockException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

