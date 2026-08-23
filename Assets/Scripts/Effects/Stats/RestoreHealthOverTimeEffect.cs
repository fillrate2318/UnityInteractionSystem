using UnityEngine;

[CreateAssetMenu(fileName = "RestoreHealthOverTimeEffect", menuName = "Scriptable Objects/RestoreHealthOverTimeEffect")]
public class RestoreHealthOverTimeEffect : Effect
{
    [SerializeField] private float healthPerSecond = 10f;
    
    public override void OnStart(InteractionContext context)
    {
        if (!context.Initiator || !context.Target) return;
        Debug.Log($"{context.Initiator.DisplayName} started healing " +
                  $"{context.Target.DisplayName}.");
    }

    public override void OnTick(InteractionContext context, float deltaTime)
    {
        base.OnTick(context, deltaTime);

        if (!context.Target) return;
        
        EntityStats stats = context.Target.GetComponent<EntityStats>();

        if (!stats)
        {
            Debug.LogWarning($"{context.Target.DisplayName} has no EntityStats component.", 
                context.Target);
            return;
        }
        
        stats.RestoreHealth(healthPerSecond * deltaTime);
    }

    public override void OnComplete(InteractionContext context)
    {
        if (!context.Target) return;
        Debug.Log($"Healing {context.Target.DisplayName} completed.");
    }

    public override void OnCancel(InteractionContext context)
    {
        if (!context.Target) return;
        Debug.Log($"Healing {context.Target.DisplayName} cancelled.");
    }
}
