using System;
using UnityEngine;

public enum InteractionKind
{
    Immediate, 
    Timed
}

[Serializable]
public struct FactionPair
{
    [SerializeField] private FactionDefinition initiatorFaction;
    [SerializeField] private FactionDefinition targetFaction;

    public FactionPair(FactionDefinition initiatorFaction, 
        FactionDefinition targetFaction)
    {
        this.initiatorFaction = initiatorFaction;
        this.targetFaction = targetFaction;
    }

    public bool Equals(FactionPair other)
    {
        return initiatorFaction == other.initiatorFaction && 
               targetFaction == other.targetFaction;
    }
    
    public override bool Equals(object obj)
    {
        return obj is FactionPair other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(initiatorFaction, targetFaction);
    }
}

[CreateAssetMenu(fileName = "InteractionDefinition", menuName = "Scriptable Objects/InteractionDefinition")]
public class InteractionDefinition : ScriptableObject
{
    [SerializeField] public string identifier;
    [SerializeField] public string description;
    [SerializeField] public InteractionKind kind;

    [SerializeField] public float duration;
    [SerializeField] public int priority;

    [SerializeField] public FactionPair[] allowedFactionPairs;
    [SerializeField] public Effect effect;
}
