using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "Scriptable Objects/Effect")]
public abstract class Effect : ScriptableObject
{
    // These methods are virtual because derived effects do not need to implement every lifecycle hook.

    // Executes when the effect is applied, before the first tick.
    public virtual void OnStart(InteractionContext context) { }

    // Executes once per frame while the effect is active.
    public virtual void OnTick(InteractionContext context, float deltaTime) { }

    // Executes after the effect finishes normally.
    public virtual void OnComplete(InteractionContext context) { }

    // Executes when an interaction with a higher-priority effect interrupts this effect.
    public virtual void OnCancel(InteractionContext context) { }
}
