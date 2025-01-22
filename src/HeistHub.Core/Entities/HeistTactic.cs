namespace HeistHub.Core.Entities;

public sealed class HeistTactic
{
    public Guid HeistId { get; private set; }

    public Heist Heist { get; private set; } = null!;

    public Guid TacticId { get; private set; }

    public Tactic Tactic { get; private set; } = null!;
}