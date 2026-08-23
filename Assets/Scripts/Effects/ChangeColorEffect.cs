using UnityEngine;

[CreateAssetMenu(fileName = "ChangeColorEffect", menuName = "Scriptable Objects/ChangeColorEffect")]
public class ChangeColorEffect : Effect
{
    [SerializeField] private Color color;
    
    public override void OnStart(InteractionContext context)
    {
        EntityVisual visual = GetVisual(context);
        if (!visual) return;
        
        visual.SetColor(color);
    }

    private EntityVisual GetVisual(InteractionContext context)
    {
        if (!context.Target) return null;
        
        return context.Target.GetComponent<EntityVisual>();
    }
}
