using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public UnityEvent<Enemy> OnEnemyDie;
    [SerializeField] private Rigidbody[] bones;
    [SerializeField] private Animator animator;
    [SerializeField] private float force;

    public void Die()
    {
        OnEnemyDie.Invoke(this);
        Invoke("Destroy", 1f);
        
        animator.enabled = false;
        foreach (var bone in bones)
        {
            bone.isKinematic = false;
            bone.AddForce(-transform.forward * force);
        }
    }

    private void Destroy()
    {
        GameObject.Destroy(gameObject);
    }
}
