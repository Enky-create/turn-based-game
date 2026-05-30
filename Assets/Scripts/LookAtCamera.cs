using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform mainCameraTransform;
    [SerializeField] private bool invert;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        mainCameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    private void LateUpdate() {
        if (invert)
        {
            var cameraDirection = (mainCameraTransform.position - transform.position).normalized;
            transform.LookAt(transform.position + cameraDirection * -1);
        }
        else
        {
            transform.LookAt(mainCameraTransform);
        }
    }
}
