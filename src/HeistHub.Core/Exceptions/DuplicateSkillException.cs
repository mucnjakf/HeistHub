namespace HeistHub.Core.Exceptions;

public sealed class DuplicateSkillException(string message) : Exception(message);