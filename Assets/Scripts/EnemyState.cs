using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    public void EnterState(EnemyState enemy);
    public void UpdateState();
    public void ExitState();
}
public class EnemyState : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
