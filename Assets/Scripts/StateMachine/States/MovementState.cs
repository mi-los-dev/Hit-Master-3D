using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : State
{
    private Vector3[] _wayPoints;
    private bool _isMoved = false;
    private int _currentRoutePoint = 1;
    
    public MovementState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        _wayPoints = new Vector3[character.WayPoints.Length];
        
        for (var i = 0; i < character.WayPoints.Length; i++)
        {
            var point = character.WayPoints[i];
            _wayPoints[i] = point.position;
        }

        character.transform.position = _wayPoints[0];
    }

    public override void Tap(Vector2 screenPos)
    {
        character.Animator.SetBool("IsRun", true);
        _isMoved = true;
    }

    public override void Update()
    {
        if (!_isMoved) return;

        if (Vector3.Distance(character.transform.position, _wayPoints[_currentRoutePoint]) > 0.25f)
        {
            character.NavMeshAgent.SetDestination(_wayPoints[_currentRoutePoint]);
        }
        else
        {
            _isMoved = false;
            _currentRoutePoint++;
            if (_currentRoutePoint >= _wayPoints.Length) 
                stateMachine.ChangeState(character.FinishState);
            else 
                stateMachine.ChangeState(character.ShootingState);
        }
    }
}