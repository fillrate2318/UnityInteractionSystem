using UnityEngine;

public class InteractionTarget
{
    public InteractionDefinition interactionDefinition;
    public Entity target;

    public int Priority => interactionDefinition.priority;
    
    public InteractionTarget(InteractionDefinition interactionDefinition, 
        Entity target)
    {
        this.interactionDefinition = interactionDefinition;
        this.target = target;
    }
}
