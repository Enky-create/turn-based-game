using UnityEngine;
using System;
using UnityEditor.IMGUI.Controls;
public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Transform emitterTransform;
    [SerializeField] private Transform bullet;
    private Vector3 shootTargetPosition;
    private Unit unit;
    private Animator animator;
    void Awake(){
        unit = GetComponent<Unit>();
        animator = unit.GetAnimator();
        if(TryGetComponent<MoveAction>(out MoveAction moveAction)){
            moveAction.OnActionStart += MoveAction_OnActionStart;
            moveAction.OnActionEnd += MoveAction_OnActionEnd;
        }
        if(TryGetComponent<ShootAction>(out ShootAction shootAction)){
            shootAction.OnActionStart+= ShootAction_OnActionStart;
        }
    }
    private void ShootAction_OnActionStart(object sender, EventArgs e)
    {
        if(e is not ShootAction.ShootEventArgs)
        {
            return;
        }
        var shootEventArgs = (ShootAction.ShootEventArgs) e;
        shootTargetPosition = shootEventArgs.target.position;
        shootTargetPosition.y =emitterTransform.position.y;
        animator.SetTrigger("FireTrigger");
        
    }
    public void ShootBullet()
    {
        var bulletInstance=Instantiate(bullet,emitterTransform.position,Quaternion.identity).GetComponent<Bullet>();
        Debug.Log($"Emitter position {emitterTransform.position}");
        
        bulletInstance.Setup(shootTargetPosition);
    }
    private void MoveAction_OnActionStart(object sender, EventArgs e)
    {
        animator.SetBool("IsWalking", true);
    }
    private void MoveAction_OnActionEnd(object sender, EventArgs e)
    {
        animator.SetBool("IsWalking", false);
    }
}
