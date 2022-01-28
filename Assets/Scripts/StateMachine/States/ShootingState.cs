using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ДЛЯ ДРУГИХ ПИСТОЛЕТОВ МЫ МОЖЕМ
// МЕНЯТЬ ВРЕМЯ ПЕРЕЗАРЯДКИ И СКОРОТЬ ПОЛЕТА ПУЛИ
// ДЛЯ ОСОБОЙ ЛОГИКИ ПИСТОЛЕТА,
// МЫ МОЖЕМ ДОБАВИТЬ НОВОЕ СОСТОЯНИЕ

public class ShootingState : State
{
    private float _rechargeTime = 0.25f; 
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
        
        var startPos = character.transform.position;
        startPos.y += 1;
        
        var bullet = character.BulletManager.GetBullet();
        bullet.transform.position = startPos;
        bullet.GetComponent<Bullet>().Drop(Po.point);

        _currentRechargeTime = _rechargeTime;
    }

    public override void Update()
    {
        if (_currentRechargeTime >= 0)
        {
            _currentRechargeTime -= Time.deltaTime;
        }
    }
}
