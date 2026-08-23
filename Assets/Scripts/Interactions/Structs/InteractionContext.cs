public readonly struct InteractionContext
{
    public Entity Initiator { get; }
    public Entity Target { get; }
    public InteractionDefinition Interaction { get; }

    public InteractionContext(Entity initiator, Entity target, InteractionDefinition interaction)
    {
        Initiator = initiator;
        Target = target;
        Interaction = interaction;
    }
}
