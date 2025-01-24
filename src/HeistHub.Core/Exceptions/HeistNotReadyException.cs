namespace HeistHub.Core.Exceptions;

public sealed class HeistNotReadyException(string message) : Exception(message);