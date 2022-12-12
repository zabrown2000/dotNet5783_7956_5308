

namespace DO;

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

