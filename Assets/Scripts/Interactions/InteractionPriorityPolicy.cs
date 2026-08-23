public static class InteractionPriorityPolicy
{
    public static bool CanInterruptInteraction(int currentPriority, int candidatePriority)
    {
        return candidatePriority > currentPriority;
    }
}
