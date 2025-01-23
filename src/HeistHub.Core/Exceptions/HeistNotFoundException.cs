namespace HeistHub.Core.Exceptions;

public sealed class HeistNotFoundException(string message) : Exception(message);