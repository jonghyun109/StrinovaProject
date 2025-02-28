using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    private IObjectPool<GameObject> _pool;
    private int health;
    private EnemyPool enemyPool;

    void Start()
    {
        enemyPool = FindObjectOfType<EnemyPool>();
        ResetEnemy();
    }

    public void SetPool(IObjectPool<GameObject> pool)
    {
        _pool = pool;
    }

    public void ResetEnemy()
    {
        health = 3; // ¸öÅë 3¹æ ¸Â¾Æ¾ß Á×À½
    }

    public void TakeDamage(bool isHeadshot)
    {
        if (isHeadshot)
        {
            Die();
        }
        else
        {
            health--;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.2f);
        enemyPool.RespawnEnemy(gameObject);
    }
}
