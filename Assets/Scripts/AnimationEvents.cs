using Unity.VisualScripting;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField]private UnitAnimator unitAnimator;
    
    public void ShootBullet()
    {
        unitAnimator.ShootBullet();
    }
}
