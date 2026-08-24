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

    [SerializeField] private Color debugColor;

    public string Identifier => identifier;
    public string Description => description;
    public InteractionKind Kind => kind;
    public float Duration => duration;
    public int Priority => priority;
    public IReadOnlyList<FactionPair> AllowedFactionPairs => allowedFactionPairs;
    public Effect Effect => effect;
    public Color DebugColor => debugColor;

    private void OnValidate()
    {
        if (kind == InteractionKind.Timed && duration <= 0)
        {
            Debug.LogWarning("Interaction definition contains a duration less than or equal to zero.", this);
        }

        if (effect == null)
        {
            Debug.LogWarning("Interaction definition contains no effect.", this);
        }

        if (string.IsNullOrWhiteSpace(identifier))
        {
            Debug.LogWarning("Interaction definition contains no identifier.", this);
        }

        if (allowedFactionPairs == null || allowedFactionPairs.Length == 0)
        {
            Debug.LogWarning("Interaction definition contains no allowed faction pairs.", this);
        }
    }
}
