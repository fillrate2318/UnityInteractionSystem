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
}

[CreateAssetMenu(fileName = "InteractionDefinition", menuName = "Scriptable Objects/InteractionDefinition")]
public class InteractionDefinition : ScriptableObject
{
    [SerializeField] private string displayName;
    [SerializeField] private string description;
    [SerializeField] private InteractionKind kind;

    [SerializeField] private float duration;
    [SerializeField] private int priority;

    [SerializeField] private FactionPair[] allowedFactionPairs;
    [SerializeField] private Effect effect;
}
