using System.Runtime.Serialization;

namespace BO;

/// <summary>
/// Default exception class
/// </summary>
public class Exceptions : Exception
{
    public Exceptions() : base() { }
    public Exceptions(string message) : base(message) { }
    public Exceptions(string message, Exception innerException) : base(message, innerException) { }
    protected Exceptions(SerializationInfo info, StreamingContext context) : base(info, context) { }
    public override string ToString() => base.ToString();

}

/// <summary>
/// Exception class for business logic layer - an entity is not found
/// </summary>
public class BOEntityDoesNotExistException : Exception
    {
        public BOEntityDoesNotExistException() { }
        public BOEntityDoesNotExistException(String message) : base(message) { }
        public BOEntityDoesNotExistException(String message, Exception inner) : base(message, inner) { }
        protected BOEntityDoesNotExistException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
/// <summary>
/// Exception class for business logic layer - input given for an entity is invalid
/// </summary>
public class InvalidInputException : Exception
    {
        public InvalidInputException() { }
        public InvalidInputException(string message) : base(message) { }
        public InvalidInputException(string message, Exception inner) : base(message, inner) { }
        protected InvalidInputException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
/// <summary>
/// Exception class for business logic layer - an entity already exists
/// </summary>
public class BOEntityAlreadyExistsException : Exception
    {
        public BOEntityAlreadyExistsException() { }
        public BOEntityAlreadyExistsException(string message) : base(message) { }
        public BOEntityAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
        protected BOEntityAlreadyExistsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
/// <summary>
/// Exception class for business logic layer - an item is out of stock
/// </summary>
public class OutOfStockException : Exception
    {
        public OutOfStockException() { }
        public OutOfStockException(string message) : base(message) { }
        public OutOfStockException(string message, Exception inner) : base(message, inner) { }
        protected OutOfStockException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

