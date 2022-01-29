using UnityEngine;

public interface IGun
{
    public void Shot(Vector3 target);
    public float GetRechargeTime();
}