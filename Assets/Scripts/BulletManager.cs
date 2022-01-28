using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject BulletPrefab;
    [SerializeField] private List<GameObject> bullets = new List<GameObject>();
    
    public GameObject GetBullet()
    {
        var disableBullets = bullets.FindAll(b => !b.activeSelf);
        if (disableBullets.Count == 0)
        {
            var bullet = Instantiate(BulletPrefab, transform);
            bullets.Add(bullet);
            return bullet;
        }
        else
        {
            var bullet = disableBullets.First();
            bullet.SetActive(true);
            return bullet;
        }
    }
}
