using UnityEngine;

[CreateAssetMenu(fileName = "ChangeColorEffect", menuName = "Scriptable Objects/ChangeColorEffect")]
public class ChangeColorEffect : Effect
{
    [SerializeField] private Color color;
    
    public override void OnStart(InteractionContext context)
    {
        EntityVisual visual = GetVisual(context);
        if (!visual) return;
     
        Color previousColor = visual.Color;
        
        if (previousColor == color)
        {
            return;
        }
        
        visual.SetColor(color);
        
        Debug.Log($"{context.Initiator.DisplayName} changed {context.Target.DisplayName} color from " +
                  $"#{ColorUtility.ToHtmlStringRGB(previousColor)} to #{ColorUtility.ToHtmlStringRGB(color)}.",
            context.Target);
    }

    private EntityVisual GetVisual(InteractionContext context)
    {
        if (!context.Target) return null;
        
        return context.Target.GetComponent<EntityVisual>();
    }
}
