using UnityEngine;

[CreateAssetMenu(fileName = "FactionDefinition", menuName = "Scriptable Objects/FactionDefinition")]
public class FactionDefinition : ScriptableObject
{
    [SerializeField] private string Name;
    [SerializeField] private string Description;
}
