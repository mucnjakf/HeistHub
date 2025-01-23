namespace HeistHub.Core.Exceptions;

public sealed class MemberNotFoundException(string message) : Exception(message);