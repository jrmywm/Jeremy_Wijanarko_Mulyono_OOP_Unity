using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    private Label healthLabel;
    private Label enemiesLeftLabel;
    private Label pointsLabel;
    private Label waveLabel;

    private EnemySpawner[] enemySpawners;
    private int totalPoints = 0;

    private Player player;
    private CombatManager combatManager;

    public void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        healthLabel = root.Q<Label>("Health");
        enemiesLeftLabel = root.Q<Label>("EnemiesLeft");
        pointsLabel = root.Q<Label>("Points");
        waveLabel = root.Q<Label>("Wave");

        enemySpawners = FindObjectsOfType<EnemySpawner>();
        player = Player.Instance;
        combatManager = FindObjectOfType<CombatManager>();
    }

    public void Update()
    {
        if (player != null && combatManager != null)
        {
            UpdateHealth();
            UpdateEnemiesLeft();
            UpdatePoints();
            UpdateWave();
        }
    }

    public void UpdateHealth()
    {
        healthLabel.text = "Health: " + player.GetComponent<HealthComponent>().Health;
    }

    public void UpdateEnemiesLeft()
    {
        enemiesLeftLabel.text = "Enemies Left: " + combatManager.totalEnemies;
    }

    public void UpdatePoints()
    {
        totalPoints = 0;
        foreach (var spawner in enemySpawners)
        {
            totalPoints += spawner.totalKill * spawner.enemyPrefab.level;
        }
        pointsLabel.text = "Points: " + totalPoints;
    }

    public void UpdateWave()
    {
        waveLabel.text = "Wave: " + combatManager.waveNumber;
    }
}