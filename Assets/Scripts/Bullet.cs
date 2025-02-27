using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Bullet : MonoBehaviour
{
    private Vector3 _dir;


    private IObjectPool<Bullet> _pool;

    public void SetPool(IObjectPool<Bullet> pool)
    {
        _pool = pool;
    }
    public void Shoot(Vector3 dir)
    {
        _dir = dir;
        Invoke("DestroyBullet", 5f);
    }
    public void DestroyBullet()
    {
        _pool.Release(this);
    }
}
