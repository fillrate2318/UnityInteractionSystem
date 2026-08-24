using UnityEngine;

public class EntityVisual : MonoBehaviour
{
    [SerializeField] private Renderer renderer;

    public Color Color => renderer ? renderer.material.color : Color.white;
    
    public void SetColor(Color color)
    {
        if (!renderer)
        {
            return;
        }
        renderer.material.color = color;
    }
}
