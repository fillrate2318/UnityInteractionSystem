using UnityEngine;

[CreateAssetMenu(fileName = "EmitLogEffect", menuName = "Scriptable Objects/EmitLogEffect")]
public class EmitLogEffect : Effect
{
    [SerializeField] private string message = "{initiator} interacted with {target}";
    
    public override void OnStart(InteractionContext context)
    {
        if (string.IsNullOrWhiteSpace(message) || !context.Initiator || !context.Target)
        {
            return;
        }
        
        string formattedMessage = message.Replace("{initiator}", context.Initiator.DisplayName)
            .Replace("{target}", context.Target.DisplayName);

        Debug.Log(formattedMessage, context.Initiator);
    }
}
