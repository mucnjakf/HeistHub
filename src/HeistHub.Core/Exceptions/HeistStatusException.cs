namespace HeistHub.Core.Exceptions;

public sealed class HeistStatusException(string message) : Exception(message);