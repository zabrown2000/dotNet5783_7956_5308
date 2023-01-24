

using System.Runtime.Serialization;

namespace DO;

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

public class EntityDoesNotExistException : Exception
{
    public EntityDoesNotExistException(Object entity) : base($"The {entity.GetType().Name} does not exist") { }
}
public class EntityAlreadyExistsException : Exception
{
    public EntityAlreadyExistsException(Object entity) : base($"The {entity.GetType().Name} already exists") { }
}

public class EntityListIsFullException : Exception
{   
    public EntityListIsFullException(Object entity) : base($"The {entity.GetType().Name} list is full") { }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}

/// <summary>
/// Exception class for dal layer - an ID does not exist
/// </summary>
[Serializable]
public class IDDoesNotExistException : Exception
{
    public IDDoesNotExistException() { }
    public IDDoesNotExistException(string message) : base(message) { }
    public IDDoesNotExistException(string message, Exception inner) : base(message, inner) { }
    protected IDDoesNotExistException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
