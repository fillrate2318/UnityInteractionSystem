using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "Scriptable Objects/Effect")]
public abstract class Effect : ScriptableObject
{
    // Virtual methods are not abstract because not each child class need to implement all methods
    
    // Executes when effect is applied, before first update call
    public virtual void OnStart(InteractionContext context) {}
    
    // Executes each frame
    public virtual void OnTick(InteractionContext context) {}
    
    // Executes after effect is finished normally
    public virtual void OnComplete(InteractionContext context) {}
    
    // Executes if effect was canceled by effect with higher priority
    public virtual void OnCancel(InteractionContext context) {}
}
