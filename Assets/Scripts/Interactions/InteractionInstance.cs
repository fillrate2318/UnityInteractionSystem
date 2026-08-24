using UnityEngine;

public class InteractionInstance
{
    private readonly InteractionContext context;

    private float elapsedTime;

    public InteractionContext Context => context;
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
        if (IsComplete)
        {
            return;
        }

        DrawDebugLine();

        float remainingTime = Definition.Duration - elapsedTime;
        float tickDeltaTime = Mathf.Min(deltaTime, remainingTime);

        if (tickDeltaTime <= 0f)
        {
            return;
        }

        Definition.Effect.OnTick(context, tickDeltaTime);
        elapsedTime += tickDeltaTime;
    }

    public void Complete()
    {
        Definition.Effect.OnComplete(context);
    }
    
    public void Cancel()
    {
        Definition.Effect.OnCancel(context);
    }

    private void DrawDebugLine()
    {
        if (!context.Initiator || !context.Target)
        {
            return;
        }

        Debug.DrawLine(context.Initiator.transform.position, context.Target.transform.position,
            Definition.DebugColor);
    }
}
