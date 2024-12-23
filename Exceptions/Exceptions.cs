namespace OpenShelter.Exceptions;

public sealed class ConfigurationException(String message) : Exception(message)
{    
}

public sealed class NotFoundException(String message) : Exception(message)
{
}

public sealed class ConflictException(String message) : Exception(message)
{    
}