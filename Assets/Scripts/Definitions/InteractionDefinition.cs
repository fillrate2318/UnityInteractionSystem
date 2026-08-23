using System;
using System.Collections.Generic;
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
    [SerializeField] private string identifier;
    [SerializeField] private string description;
    [SerializeField] private InteractionKind kind;

    [SerializeField] private float duration;
    [SerializeField] private int priority;

    [SerializeField] private FactionPair[] allowedFactionPairs;
    [SerializeField] private Effect effect;

    public string Identifier => identifier;
    public string Description => description;
    public InteractionKind Kind => kind;
    public float Duration => duration;
    public int Priority => priority;
    public IReadOnlyList<FactionPair> AllowedFactionPairs => allowedFactionPairs;
    public Effect Effect => effect;
}
