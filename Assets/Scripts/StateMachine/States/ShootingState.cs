using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : State
{
    private float _currentRechargeTime;
    
    public ShootingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }
    
    public override void Enter()
    {
        character.Animator.SetBool("IsRun", false);
    }

    public override void Tap(Vector2 screenPos)
    {
        if (_currentRechargeTime > 0) return;
        
        var ray = character.Camera.ScreenPointToRay(screenPos);
        RaycastHit Po;
        if (!Physics.Raycast(ray, out Po)) return;
        
        character.Gun.Shot(Po.point);

        _currentRechargeTime = character.Gun.GetRechargeTime();
    }

    public override void Update()
    {
        if (_currentRechargeTime >= 0)
        {
            _currentRechargeTime -= Time.deltaTime;
        }
    }
}
