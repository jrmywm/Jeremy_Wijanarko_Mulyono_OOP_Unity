using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;
    public float timer = 0f;

    public List<EnemySpawner> spawners;

    private void Awake()
    {
        InitializeWave();
    }

    private void Update()
    {
        if (totalEnemies == 0 || waveNumber == 1)
        {
            timer += Time.deltaTime;
            if (timer >= waveInterval)
            {
                StartNextWave();
            }
        }
    }

    public void EnemyDefeated()
    {
        totalEnemies--;
        // Update UI when an enemy is defeated
        FindObjectOfType<MainUI>().UpdateEnemiesLeft();
    }

    private void InitializeWave()
    {
        waveNumber = 1;
        timer = 0f;
    }

    private void StartNextWave()
    {
        timer = 0f;
        waveNumber++;
        totalEnemies = 0;

        foreach (EnemySpawner spawner in spawners)
        {
            spawner.BeginSpawning();
        }

        // Update UI when a new wave starts
        FindObjectOfType<MainUI>().UpdateWave();
    }

    public void RegisterEnemy()
    {
        totalEnemies++;
        // Update UI when a new enemy is registered
        FindObjectOfType<MainUI>().UpdateEnemiesLeft();
    }
}