using UnityEngine;

public abstract class Effect : ScriptableObject
{
    [SerializeField] private Color debugColor;
    
    // These methods are virtual because derived effects do not need to implement every lifecycle hook.

    // Executes when the effect is applied, before the first tick.
    public virtual void OnStart(InteractionContext context) { }

    // Executes once per frame while the effect is active.
    public virtual void OnTick(InteractionContext context, float deltaTime)
    {
        if (context.Initiator && context.Target)
        {
            Debug.DrawLine(context.Initiator.transform.position, context.Target.transform.position, 
                debugColor);
        }
    }

    // Executes after the effect finishes normally.
    public virtual void OnComplete(InteractionContext context) { }

    // Executes when a higher-priority interaction interrupts this interaction.
    public virtual void OnCancel(InteractionContext context) { }
}
