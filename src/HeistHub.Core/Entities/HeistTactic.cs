namespace HeistHub.Core.Entities;

public sealed class HeistTactic
{
    public Guid HeistId { get; init; }

    public Heist Heist { get; private set; } = null!;

    public Guid TacticId { get; init; }

    public Tactic Tactic { get; private set; } = null!;
}