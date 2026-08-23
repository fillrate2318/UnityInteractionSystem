using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class InteractionSelector
{
    public static InteractionTarget SelectInteractionTarget(IReadOnlyList<InteractionTarget> targets)
    {
        return targets.OrderByDescending(t => t.interactionDefinition.priority).
            ThenBy(t => t.interactionDefinition.identifier, StringComparer.Ordinal).
            ThenBy(t => t.target.DisplayName, StringComparer.Ordinal).FirstOrDefault();
    }
}
