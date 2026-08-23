using UnityEngine;

public struct InteractionContext
{
    public Entity initiator;
    public Entity target;

    public InteractionContext(Entity initiator, Entity target)
    {
        this.initiator = initiator;
        this.target = target;
    }

    public void DrawDebug(Color color, float duration)
    {
        if (initiator && target)
        {
            Debug.DrawLine(initiator.transform.position, target.transform.position, 
                color, duration);
        }
    }
}
