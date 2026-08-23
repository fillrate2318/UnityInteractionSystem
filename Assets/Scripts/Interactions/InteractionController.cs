using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionController
{
    private Entity owner;
    private InteractionInstance currentInteraction;
    
    public InteractionController(Entity owner)
    {
        this.owner = owner;
    }

    public void Tick(float deltaTime)
    {
        if (currentInteraction != null)
        {
            currentInteraction.Tick(deltaTime);
            if (currentInteraction.IsComplete)
            {
                CompleteInteraction();
            }
        }
    }
    
    public void EvaluateInteractions()
    {
        IReadOnlyList<Entity> entities = EntityRegistry.Entities;
        List<InteractionTarget> targets = new List<InteractionTarget>();
        foreach (Entity entity in entities)
        {
            if (entity == owner) continue;

            FactionPair factionPair = new FactionPair(owner.Faction, entity.Faction);
            foreach (InteractionDefinition interactionDefinition in owner.Interactions)
            {
                if (interactionDefinition.allowedFactionPairs.Contains(factionPair))
                {
                    targets.Add(new InteractionTarget(interactionDefinition, entity));
                }
            }
        }

        InteractionTarget target = InteractionSelector.SelectInteractionTarget(targets);
        if (target == null) return;
        
        TryToStartInteraction(target);
    }

    void TryToStartInteraction(InteractionTarget interactionTarget)
    {
        if (currentInteraction != null)
        {
            // Early return if current interaction has higher prority
            if (interactionTarget.Priority <= currentInteraction.Definition.priority)
            {
                return;
            }
            
            CancelInteraction();
        }
        
        currentInteraction = new InteractionInstance(interactionTarget.interactionDefinition, 
            new InteractionContext(owner, interactionTarget.target));
        
        currentInteraction.Start();
        if (currentInteraction.Definition.kind == InteractionKind.Immediate)
        {
            CompleteInteraction();
        }
    }
    
    private void CompleteInteraction()
    {
        currentInteraction.Complete();
        currentInteraction = null;
    }

    private void CancelInteraction()
    {
        currentInteraction.Cancel();
        currentInteraction = null;
    }
}
