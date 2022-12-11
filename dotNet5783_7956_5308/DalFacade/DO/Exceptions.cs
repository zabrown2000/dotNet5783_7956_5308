

namespace DO;

public class EntityDoesNotExistException : Exception
{
    public EntityDoesNotExistException(String message) : base(message) { }
}
public class EntityAlreadyExistsException : Exception
{
    public EntityAlreadyExistsException(String message) : base(message) { }
}

public class EntityListIsFullException : Exception
{
    public EntityListIsFullException(String message) : base(message) { }
}

