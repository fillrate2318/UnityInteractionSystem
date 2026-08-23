using UnityEngine;

[CreateAssetMenu(fileName = "EmitLogEffect", menuName = "Scriptable Objects/EmitLogEffect")]
public class EmitLogEffect : Effect
{
    public override void OnStart()
    {
        Debug.Log("Log effect applied");
    }
}
