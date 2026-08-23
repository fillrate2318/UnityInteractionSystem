using UnityEngine;

[CreateAssetMenu(fileName = "ChangeColorEffect", menuName = "Scriptable Objects/ChangeColorEffect")]
public class ChangeColorEffect : Effect
{
    [SerializeField] private Color color;
    
    public override void OnStart(InteractionContext context)
    {
        if (context.target && context.initiator)
        {
            context.DrawDebug(Color.cadetBlue, 0.2f);
            EntityVisual visual = context.target.GetComponent<EntityVisual>();
            if (visual)
            {
                visual.SetColor(color);
            }
        }
    }
}
