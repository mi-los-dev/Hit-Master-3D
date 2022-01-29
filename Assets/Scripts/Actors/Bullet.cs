using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    private Coroutine _coroutine;

    public void Drop(Vector3 targetPos)
    {
        _coroutine = StartCoroutine(Move(targetPos));
    }

    private IEnumerator Move(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * Speed);
            yield return null;
        }
        
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.ENEMY_BODY))
        {
            other.GetComponent<Enemy>().Die();
        }

        if (!other.CompareTag(Tags.ENEMY_NEAR_DISTANCE) && !other.CompareTag(Tags.PLAYER))
        {
            StopCoroutine(_coroutine);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.MAP))
        {
            StopCoroutine(_coroutine);
            gameObject.SetActive(false);
        }
    }
}
