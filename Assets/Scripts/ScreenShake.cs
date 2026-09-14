using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;
public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance{private set; get;}
    private CinemachineImpulseSource  cinemachineImpulseSource;
    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        if(Instance is not null)
        {
            Debug.LogError("There are two instances of ScreenShake");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void Shake(float shakeForce=3)
    {
        cinemachineImpulseSource.GenerateImpulse(shakeForce);
    }
}
