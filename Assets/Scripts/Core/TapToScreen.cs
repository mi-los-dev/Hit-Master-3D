using UnityEngine;
using UnityEngine.Events;

public class TapToScreen : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector2> OnTap;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnTap.Invoke(Input.mousePosition);
        }
    }
}
