using System.Collections.Generic;
using System.Linq;

public static class InteractionAvailability
{
    public static bool IsInteractionAvailable(InteractionDefinition interaction,
        IReadOnlyList<InteractionDefinition> availableInteractions,
        FactionDefinition initiatorFaction, FactionDefinition targetFaction)
    {
        if (interaction == null || availableInteractions == null ||
            initiatorFaction == null || targetFaction == null)
        {
            return false;
        }

        if (!availableInteractions.Contains(interaction))
        {
            return false;
        }

        if (interaction.AllowedFactionPairs == null)
        {
            return false;
        }

        FactionPair pair = new FactionPair(initiatorFaction, targetFaction);
        return interaction.AllowedFactionPairs.Contains(pair);
    }
}
