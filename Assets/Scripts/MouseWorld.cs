using UnityEngine;
using UnityEngine.Rendering;

public class MouseWorld : MonoBehaviour
{
    [SerializeField] private LayerMask mouseLayerMask;
    private static MouseWorld instance;
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        transform.position = MouseWorld.MousePosition();
    }
    public static Vector3 MousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, instance.mouseLayerMask);
        return hitInfo.point;
    }
}
