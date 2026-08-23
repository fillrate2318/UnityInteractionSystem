public class InteractionInstance
{
    private readonly InteractionDefinition definition;
    private readonly InteractionContext context;

    private float elapsedTime;

    public InteractionDefinition Definition => definition;
    public int Priority => definition.Priority;

    public bool IsComplete => elapsedTime >= definition.Duration;

    public InteractionInstance(InteractionDefinition definition, InteractionContext context)
    {
        this.definition = definition;
        this.context = context;
    }

    public void Start()
    {
        definition.Effect.OnStart(context);
    }

    public void Tick(float deltaTime)
    {
        elapsedTime += deltaTime;
        definition.Effect.OnTick(context, deltaTime);
    }

    public void Complete()
    {
        definition.Effect.OnComplete(context);
    }
    
    public void Cancel()
    {
        definition.Effect.OnCancel(context);
    }
}
