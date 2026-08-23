using System.Collections.Generic;

public class InteractionController
{
    private readonly Entity owner;
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
        List<InteractionCandidate> candidates = new List<InteractionCandidate>();
        foreach (Entity entity in entities)
        {
            if (entity == owner)
            {
                continue;
            }

            foreach (InteractionDefinition interaction in owner.Interactions)
            {
                if (InteractionAvailability.IsInteractionAvailable(interaction,
                        owner.Interactions, owner.Faction, entity.Faction))
                {
                    candidates.Add(new InteractionCandidate(interaction, entity));
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

            CancelInteraction();
        }

        currentInteraction = new InteractionInstance(interactionCandidate.Interaction,
            new InteractionContext(owner, interactionCandidate.Target));

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

    private void CancelInteraction()
    {
        currentInteraction.Cancel();
        currentInteraction = null;
    }
}
