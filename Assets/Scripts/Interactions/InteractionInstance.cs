using Unity.VisualScripting;
using UnityEngine;

public class InteractionInstance
{
    private InteractionDefinition definition;
    private InteractionContext context;

    private float elapsedTime = 0;

    public InteractionDefinition Definition => definition;
    public bool IsComplete => elapsedTime >= definition.duration;
    
    public InteractionInstance(InteractionDefinition definition, InteractionContext context)
    {
        this.definition = definition;
        this.context = context;
    }
    
    public void Start()
    {
        definition.effect.OnStart(context);
    }

    public void Tick(float deltaTime)
    {
        elapsedTime += deltaTime;
        definition.effect.OnTick(context);
    }

    public void Complete()
    {
        definition.effect.OnComplete(context);
    }
    
    public void Cancel()
    {
        definition.effect.OnCancel(context);
    }
}
