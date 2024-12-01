using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public CombatManager combatManager;
    public EnemySpawner spawner;
    public int level=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (spawner != null && combatManager != null)
        {
            spawner.OnEnemyKilled();
            combatManager.OnEnemyKilled();
        }
    }
}
