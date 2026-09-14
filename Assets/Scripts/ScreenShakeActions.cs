using System;
using UnityEngine;

public class ScreenShakeActions : MonoBehaviour
{
    
    void Start()
    {
        ShootAction.OnAnyShoot+=ShootAction_OnAnyShoot;
    }

    private void ShootAction_OnAnyShoot(object sender, ShootAction.ShootEventArgs e)
    {
        ScreenShake.Instance.Shake();
    }
    void OnDestroy()
    {
        ShootAction.OnAnyShoot-=ShootAction_OnAnyShoot;
    }
}
