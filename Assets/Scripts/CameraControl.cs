using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    [SerializeField] private int rotationSpeed;
    [SerializeField]private LayerMask layerMask;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move!!1
        var moveVector = new Vector3(0,0,0);
        if (Input.GetKey(KeyCode.W)) moveVector.z += 1;
        if (Input.GetKey(KeyCode.S)) moveVector.z -= 1;
        if (Input.GetKey(KeyCode.A)) moveVector.x -= 1;
        if (Input.GetKey(KeyCode.D)) moveVector.x += 1;
        transform.position += moveVector.normalized * moveSpeed * Time.deltaTime;
        //Rotation
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, layerMask))
        {
            var rotationVector = (hitInfo.point-transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, rotationVector, rotationSpeed * Time.deltaTime);
        }
        
    }
}
