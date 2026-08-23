public sealed class InteractionCandidate
{
    public InteractionDefinition Interaction { get; }
    public Entity Target { get; }

    public int Priority => Interaction.Priority;

    public InteractionCandidate(InteractionDefinition interaction,
        Entity target)
    {
        Interaction = interaction;
        Target = target;
    }
}
