namespace HeistHub.Core.Exceptions;

public sealed class DuplicateTacticException(string message) : Exception(message);