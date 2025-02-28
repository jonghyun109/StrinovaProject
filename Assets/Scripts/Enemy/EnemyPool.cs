using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab; // 적 프리팹
    public int maxEnemies = 20; // 최대 적 개수
    private int activeEnemyCount = 0; // 현재 활성화된 적 개수

    public Vector3 spawnAreaMin;
    public Vector3 spawnAreaMax; 

    private IObjectPool<GameObject> _pool;

    void Awake()
    {
        //풀링
        _pool = new ObjectPool<GameObject>(
            CreateEnemy, OnGetEnemy, OnReleaseEnemy, OnDestroyEnemy, false, maxEnemies, maxEnemies);        
    }

    //enemy소환
    private GameObject CreateEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab);

        Enemy enemyComponet = enemy.GetComponent<Enemy>();

        enemyComponet.SetPool(_pool);
        enemy.SetActive(false);
        return enemy;
    }

    //갖고있기
    private void OnGetEnemy(GameObject enemy)
    {
        if (activeEnemyCount >= maxEnemies) return; // 최대 개수 초과 시 리턴

        enemy.SetActive(true);
        enemy.transform.position = GetSpawnPosition();
        enemy.GetComponent<Enemy>().ResetEnemy();
        activeEnemyCount++;
    }

    private void OnReleaseEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        activeEnemyCount--;
    }

    private void OnDestroyEnemy(GameObject enemy)
    {
        Destroy(enemy);
    }
        
    private Vector3 GetSpawnPosition()
    {
        return new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
        );
    }

    // 게임이 시작되면 첫 번째 적 소환
    public void SpawnFirstEnemy()
    {
        _pool.Get();
    }
    //리스폰
    public void RespawnEnemy(GameObject enemy)
    {        
        enemy.transform.position = GetSpawnPosition();
        enemy.GetComponent<Enemy>().ResetEnemy();
        _pool.Release(enemy);
        enemy.SetActive(true);
    }

}
