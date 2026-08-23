using UnityEngine;

[CreateAssetMenu(fileName = "ApplyDamageEffect", menuName = "Scriptable Objects/ApplyDamageEffect")]
public class ApplyDamageEffect : Effect
{
    [SerializeField] private float damageAmount = 25f;

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

        stats.ApplyDamage(damageAmount);

        float actualDamage = previousHealth - stats.Health;

        Debug.Log(
            $"{context.Initiator.DisplayName} applied {actualDamage:0.##} damage to " +
            $"{context.Target.DisplayName}.", context.Target);
    }
}
