using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject actionCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BaseAction.OnAnyActionStart+=BaseAction_OnAnyActionStart;
        BaseAction.OnAnyActionEnd+=BaseAction_OnAnyActionEnd;
        Hide();
    }
    private void BaseAction_OnAnyActionEnd(object sender, EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shoot:
            Hide();
            break;
        }
    }
    private void BaseAction_OnAnyActionStart(object sender, EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shoot:
                var unit = shoot.GetUnit();
                var targetUnit = shoot.GetTargetUnit();
                var aimingDirection = (targetUnit.transform.position - unit.transform.position).normalized;
                var shoulderHeight = Vector3.up *1.7f;
                Vector3 shoulderOffset = Quaternion.Euler(0,90,0)*aimingDirection * 0.5f;
                actionCamera.transform.position= 
                    unit.transform.position
                 +  shoulderHeight
                 +  shoulderOffset
                 +  aimingDirection * -1;
                actionCamera.transform.LookAt(targetUnit.transform.position + shoulderHeight);
                Show();
            break;
        }
    }
    public void Show()
    {
        actionCamera.SetActive(true);
        Debug.Log("AAA");
    }
    public void Hide()
    {
        actionCamera.SetActive(false);
    }
    void OnDestroy()
    {
        BaseAction.OnAnyActionStart-=BaseAction_OnAnyActionStart;
        BaseAction.OnAnyActionStart-=BaseAction_OnAnyActionEnd;
    }
}
