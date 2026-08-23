using UnityEngine;

[CreateAssetMenu(fileName = "EmitLogEffect", menuName = "Scriptable Objects/EmitLogEffect")]
public class EmitLogEffect : Effect
{
    public override void OnStart(InteractionContext context)
    {
        if (context.initiator && context.target)
        {
            context.DrawDebug(Color.crimson, 0.2f);
            Debug.Log($"Emit log effect applied by {context.initiator} on {context.target}");
        }
    }
}
