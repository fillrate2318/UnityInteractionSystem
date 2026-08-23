using System.Linq;

public static class InteractionAvailability
{
    public static bool IsFactionPairAllowed(InteractionDefinition interaction,
        FactionDefinition initiatorFaction, FactionDefinition targetFaction)
    {
        if (interaction == null || initiatorFaction == null || targetFaction == null)
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
