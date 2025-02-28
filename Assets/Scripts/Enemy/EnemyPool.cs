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
        _pool = new ObjectPool<GameObject>(
            CreateEnemy, OnGetEnemy, OnReleaseEnemy, OnDestroyEnemy, false, maxEnemies, maxEnemies);
        for (int i = 0; i < maxEnemies; i++)
        {
            GameObject enemy = _pool.Get();
            if (enemy != null)
            {
                _pool.Release(enemy);
                Debug.Log($"초기 적 풀링 완료: {enemy.name}");
            }
            else
            {
                Debug.LogError("2222");
            }
            Debug.Log("초기 풀 개수: " + _pool.CountInactive);
        }
    }

    private GameObject CreateEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab);

        Enemy enemyComponet = enemy.GetComponent<Enemy>();

        enemyComponet.SetPool(_pool);
        enemy.SetActive(false);
        return enemy;
    }

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
        Debug.Log($"SpawnFirstEnemy() 호출됨! maxEnemies: {maxEnemies}, 풀 개수: {_pool?.CountInactive ?? -1}");
        if (_pool == null)
        {
            Debug.LogError("ObjectPool이 초기화되지 않았습니다! `Awake()`에서 `_pool`이 설정되었는지 확인하세요!");
            return;
        }
        if (_pool.CountInactive == 0)
        {
            Debug.LogError("풀에 남아 있는 적이 없음");
        }

        _pool.Get();
    }
    public void RespawnEnemy(GameObject enemy)
    {
        if (activeEnemyCount >= maxEnemies) return; 

        enemy.transform.position = GetSpawnPosition();
        enemy.GetComponent<Enemy>().ResetEnemy();
        _pool.Release(enemy);
    }

}
