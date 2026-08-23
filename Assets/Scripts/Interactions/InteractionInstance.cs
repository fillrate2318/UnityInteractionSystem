public class InteractionInstance
{
    private readonly InteractionContext context;

    private float elapsedTime;

    public InteractionDefinition Definition => context.Interaction;
    public int Priority => Definition.Priority;

    public bool IsComplete => elapsedTime >= Definition.Duration;

    public InteractionInstance(InteractionContext context)
    {
        this.context = context;
    }

    public void Start()
    {
        Definition.Effect.OnStart(context);
    }

    public void Tick(float deltaTime)
    {
        elapsedTime += deltaTime;
        Definition.Effect.OnTick(context, deltaTime);
    }

    public void Complete()
    {
        Definition.Effect.OnComplete(context);
    }
    
    public void Cancel()
    {
        Definition.Effect.OnCancel(context);
    }
}
