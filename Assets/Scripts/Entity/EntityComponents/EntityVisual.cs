using UnityEngine;

public class EntityVisual : MonoBehaviour
{
    [SerializeField] private Renderer renderer;

    public Color color => renderer.material.color;
    
    public void SetColor(Color color)
    {
        if (!renderer) return;
        renderer.material.color = color;
    }
}
