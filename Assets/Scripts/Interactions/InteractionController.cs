using UnityEngine;

public class InteractionController
{
    private Entity owner;
    
    public InteractionController(Entity owner)
    {
        this.owner = owner;
    }

    public void EvaluateInteractions()
    {
        Debug.Log("Attempt to interact");
    }
}
