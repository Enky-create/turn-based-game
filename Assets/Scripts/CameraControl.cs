using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private const float MIN_FOLLOW_OFFSET_Y = 2f;
    private const float MAX_FOLLOW_OFFSET_Y = 12f;
    [SerializeField] private CinemachineFollow cinemachineFollow;
    [SerializeField] private int moveSpeed;
    [SerializeField] private int rotationSpeed;
    [SerializeField] private float zoomAmount;
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
        transform.position += (transform.right* moveVector.x + transform.forward * moveVector.z) * moveSpeed * Time.deltaTime;
        //Rotation
        var rotationVector = new Vector3(0,0,0);
        if(Input.GetKey(KeyCode.E)) rotationVector.y += 1;
        if (Input.GetKey(KeyCode.Q)) rotationVector.y -= 1;
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
        var cinimachineOffset = cinemachineFollow.FollowOffset;
        if (Input.mouseScrollDelta.y != 0)
        {
            cinimachineOffset.y += Input.mouseScrollDelta.y * zoomAmount;
        }
        cinimachineOffset.y = Mathf.Clamp(cinimachineOffset.y, MIN_FOLLOW_OFFSET_Y, MAX_FOLLOW_OFFSET_Y);
        cinemachineFollow.FollowOffset = Vector3.Lerp(cinemachineFollow.FollowOffset, cinimachineOffset, 10);
    }
}
