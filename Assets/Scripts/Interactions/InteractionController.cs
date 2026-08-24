using System.Collections.Generic;
using UnityEngine;

public class InteractionController
{
    private readonly Entity owner;
    private InteractionInstance currentInteraction;
    
    private readonly HashSet<(InteractionDefinition Interaction, Entity Target)> loggedRejections = new();
    
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
        if (owner.Interactions == null)
        {
            return;
        }
        
        IReadOnlyList<Entity> entities = EntityRegistry.Entities;
        List<InteractionCandidate> candidates = new List<InteractionCandidate>();
        foreach (Entity entity in entities)
        {
            if (!entity) continue;
            
            if (entity == owner) continue;
            
            foreach (InteractionDefinition interaction in owner.Interactions)
            {
                if (!interaction) continue;

                if (!interaction.Effect) continue;
                
                if (InteractionAvailability.IsFactionPairAllowed(interaction, 
                        owner.Faction, entity.Faction))
                {
                    candidates.Add(new InteractionCandidate(interaction, entity));
                }
                else
                {
                    LogRejectedInteraction(interaction, entity);
                }
            }
        }

        InteractionCandidate candidate = InteractionSelector.SelectInteractionCandidate(candidates);
        if (candidate == null)
        {
            return;
        }

        TryToStartInteraction(candidate);
    }

    private void TryToStartInteraction(InteractionCandidate interactionCandidate)
    {
        if (currentInteraction != null)
        {
            // Keep the current interaction unless the candidate has a strictly higher priority.
            if (!InteractionPriorityPolicy.CanInterruptInteraction(currentInteraction.Priority,
                    interactionCandidate.Priority))
            {
                return;
            }

            CancelInteraction(interactionCandidate);
        }
        
        currentInteraction = new InteractionInstance(new InteractionContext(owner, 
            interactionCandidate.Target, interactionCandidate.Interaction));
        
        currentInteraction.Start();
        if (currentInteraction.Definition.Kind == InteractionKind.Immediate)
        {
            CompleteInteraction();
        }
    }
    
    private void CompleteInteraction()
    {
        currentInteraction.Complete();
        currentInteraction = null;
    }

    private void CancelInteraction(InteractionCandidate interrupter)
    {
        currentInteraction.Cancel();
        
        Debug.Log($"[Interaction] '{currentInteraction.Definition.Identifier}' was cancelled by " +
                  $"'{interrupter.Interaction.Identifier}' " +
                  $"({currentInteraction.Priority} -> {interrupter.Priority}).", owner);
        
        currentInteraction = null;
    }

    private void LogRejectedInteraction(InteractionDefinition interaction, Entity target)
    {
        if (!loggedRejections.Add((interaction, target)))
        {
            return;
        }

        if (!owner.Faction || !target.Faction)
        {
            return;
        }
        
        Debug.Log($"Interaction '{interaction.Identifier}' rejected: faction pair {owner.Faction.Identifier} -> " +
                  $"{target.Faction.Identifier} is not allowed.", owner);
    }
}
