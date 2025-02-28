using System.Collections;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public GameObject etcs;
    public EnemyPool enemyPool;
    private bool gameStarted = false;

    private void Start()
    {
        enemyPool.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameStarted && other.CompareTag("StartTarget"))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        
        gameStarted = true;
        enemyPool.enabled = true;

        enemyPool.SpawnFirstEnemy();
        etcs.SetActive(true);
    }
}
