using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy enemyPrefab;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    public int totalKillWave = 0;

    [SerializeField] private float spawnDelay = 3f;

    [Header("Spawn Settings")]
    public int initialSpawnCount = 1;
    private int currentSpawnCount;
    public int spawnMultiplier = 1;
    public int multiplierIncrement = 1;

    public CombatManager combatManager;

    private bool spawningActive = false;

    private void Start()
    {
        currentSpawnCount = initialSpawnCount;
    }

    public void BeginSpawning()
    {
        if (!spawningActive && enemyPrefab.level <= combatManager.waveNumber)
        {
            spawningActive = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < currentSpawnCount; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
            enemy.spawner = this;
            combatManager.RegisterEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
        spawningActive = false;
    }

    public void EnemyKilled()
    {
        totalKill++;
        totalKillWave++;
        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            totalKillWave = 0;
            currentSpawnCount += spawnMultiplier;
        }

        // Update UI when an enemy is killed
        FindObjectOfType<MainUI>().UpdatePoints();
    }
}