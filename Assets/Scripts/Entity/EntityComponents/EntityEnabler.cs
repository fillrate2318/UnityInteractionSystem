using System.Collections;
using UnityEngine;

public class EntityEnabler : MonoBehaviour
{
    [SerializeField] private float delay = 3f;
    [SerializeField] private Entity target;
    
    void OnEnable()
    {
        if (!target)
        {
            return;
        }
        StartCoroutine(DelayAction(delay));
    }

    IEnumerator DelayAction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        target.enabled = true;
        
        Debug.Log($"{target.DisplayName} became active after {delay:0.##} seconds.", target);
    }
}
