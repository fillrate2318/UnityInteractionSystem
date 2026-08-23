using System;
using System.Collections.Generic;
using System.Linq;

public static class InteractionSelector
{
    public static InteractionCandidate SelectInteractionCandidate(
        IReadOnlyList<InteractionCandidate> candidates)
    {
        return candidates
            .OrderByDescending(candidate => candidate.Priority)
            .ThenBy(candidate => candidate.Interaction.Identifier, StringComparer.Ordinal)
            .ThenBy(candidate => candidate.Target.DisplayName, StringComparer.Ordinal)
            .FirstOrDefault();
    }
}
