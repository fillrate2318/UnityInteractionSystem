using UnityEngine;

[CreateAssetMenu(fileName = "FactionDefinition", menuName = "Scriptable Objects/FactionDefinition")]
public class FactionDefinition : ScriptableObject
{
    [SerializeField] private string identifier;
    [SerializeField] private string description;
}
