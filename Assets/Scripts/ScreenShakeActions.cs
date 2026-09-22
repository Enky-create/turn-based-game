using System;
using UnityEngine;

public class ScreenShakeActions : MonoBehaviour
{
    
    void Start()
    {
        ShootAction.OnAnyShoot+=ShootAction_OnAnyShoot;
        Grenade.ON_ANY_GRENADE_EXPLOTION+=Grenade_ON_ANY_GRENADE_EXPLOTION;
    }

    private void Grenade_ON_ANY_GRENADE_EXPLOTION(object sender, EventArgs e)
    {
        ScreenShake.Instance.Shake(15);
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
