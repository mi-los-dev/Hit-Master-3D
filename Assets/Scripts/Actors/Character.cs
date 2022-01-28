using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public BulletManager BulletManager;
    public Transform[] WayPoints;
    public Animator Animator;
    public StateMachine StateMachine { get; private set; }
    public ShootingState ShootingState { get; private set; }
    public MovementState MovementState { get; private set; }
    public FinishState FinishState { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    public Camera Camera { get; private set; }
    private List<Enemy> _enemies = new List<Enemy>();

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Camera = Camera.main;
    }

    private void Start()
    {
        StateMachine = new StateMachine();
        ShootingState = new ShootingState(this, StateMachine);
        MovementState = new MovementState(this, StateMachine);
        FinishState = new FinishState(this, StateMachine);
        StateMachine.Initialize(MovementState);
    }

    public void Taped(Vector2 screenPos)
    {
        StateMachine.CurrentState.Tap(screenPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.ENEMY_NEAR_DISTANCE))
        {
            var enemy = other.transform.parent.GetComponent<Enemy>();
            _enemies.Add(enemy);
            enemy.OnEnemyDie.AddListener(EnemyDied);
        }
    }

    private void EnemyDied(Enemy enemy)
    {
        _enemies.Remove(enemy);
        if (_enemies.Count < 1)
        {
            StateMachine.ChangeState(MovementState);
        }
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
    }
}
