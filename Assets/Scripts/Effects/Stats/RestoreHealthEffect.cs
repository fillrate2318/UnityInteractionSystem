using UnityEngine;

[CreateAssetMenu(fileName = "RestoreHealthEffect", menuName = "Scriptable Objects/RestoreHealthEffect")]
public class RestoreHealthEffect : Effect
{
    [SerializeField] private float healthAmount = 25f;

    public override void OnStart(InteractionContext context)
    {
        if (!context.Initiator || !context.Target) return;
        
        EntityStats stats = context.Target.GetComponent<EntityStats>();

        if (!stats)
        {
            Debug.LogWarning(
                $"{context.Target.DisplayName} has no EntityStats component.",
                context.Target);
            return;
        }

        float previousHealth = stats.Health;

        stats.RestoreHealth(healthAmount);

        float restoredAmount = stats.Health - previousHealth;

        Debug.Log(
            $"{context.Initiator.DisplayName} restored {restoredAmount:0.##} health to " +
            $"{context.Target.DisplayName}.", context.Target);
    }
}
