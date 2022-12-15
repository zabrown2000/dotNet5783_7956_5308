namespace BO;



    public class BOEntityAlreadyExistsException : Exception
    {
        public BOEntityAlreadyExistsException(Object entity) : base($"The {entity.GetType().Name} already exists") { }
    }

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
    public class OutOfStockException : Exception
    {
        public OutOfStockException() { }
        public OutOfStockException(string message) : base(message) { }
        public OutOfStockException(string message, Exception inner) : base(message, inner) { }
        protected OutOfStockException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public class OrderTrackingException : Exception
    {
        public OrderTrackingException() { }
        public OrderTrackingException(string message) : base(message) { }
        public OrderTrackingException(string message, Exception inner) : base(message, inner) { }
        protected OrderTrackingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

