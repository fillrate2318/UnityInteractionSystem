using UnityEngine;

public class EntityVisual : MonoBehaviour
{
    [SerializeField] private Renderer renderer;

    public void SetColor(Color color)
    {
        renderer.material.color = color;
    }
}
