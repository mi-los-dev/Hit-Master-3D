using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IGun
{
    [SerializeField] private float rechargeTime = 0.25f;
    private BulletManager bulletManager;

    private void Awake()
    {
        bulletManager = FindObjectOfType<BulletManager>(); 
        // ОПЕРАЦИЯ ДОЛГАЯ, НО ЮЗАЮ 1 РАЗ, НА СЛУЧАЙ,
        // ЕСЛИ ПИСТОЛЕТ БУДЕТ СОЗДАВАТЬСЯ НА СЦЕНЕ
        // ВО ВРЕМЯ ИГРЫ
    }

    public void Shot(Vector3 target)
    {
        var startPos = transform.position;
        startPos.y += 1;
        
        var bullet = bulletManager.GetBullet();
        bullet.transform.position = startPos;
        bullet.GetComponent<Bullet>().Drop(target);
    }

    public float GetRechargeTime() => rechargeTime;
}
