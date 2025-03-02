using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    public bool randomSpawn = true;
    private GameManager gameManager;
    public GameObject enemyPrefab; // 적 프리팹
    public int maxEnemies = 20; // 최대 적 개수
    private int activeEnemyCount = 0; // 현재 활성화된 적 개수

    public Vector3 spawnAreaMin;
    public Vector3 spawnAreaMax; 

    private IObjectPool<GameObject> _pool;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
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
        enemy.SetActive(true);
        enemy.transform.position = GetSpawnPosition();
        enemy.GetComponent<Enemy>().ResetEnemy();

        // 적 개수 UI 업데이트
        FindObjectOfType<GameManager>().UpdateEnemyCountUI();
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
        if (randomSpawn)
        {
            return new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );
        }
        else
        {
            return new Vector3(spawnAreaMin.x, spawnAreaMin.y, spawnAreaMin.z);
        }
    }

    // 게임이 시작되면 첫 번째 적 소환
    public void SpawnFirstEnemy()
    {       
        if(activeEnemyCount< maxEnemies)
        {
            _pool.Get();
            activeEnemyCount++;
        }        
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
