using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LevelManager LevelManager { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        LevelManager = GetComponentInChildren<LevelManager>();

        if (LevelManager == null)
        {
            Debug.LogError("LevelManager not found in children of GameManager.");
        }

        DontDestroyOnLoad(gameObject);

        GameObject camera = GameObject.Find("Main Camera");
        if (camera != null)
        {
            DontDestroyOnLoad(camera);
        }
        else
        {
            Debug.LogError("Camera not found in the scene.");
        }

        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            DontDestroyOnLoad(player);
        }
        else
        {
            Debug.LogError("Player not found in the scene.");
        }
    }

    public void ClearScene()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name != "Main Camera" && obj.name != "Player" && obj != gameObject)
            {
                Destroy(obj);
            }
        }
    }
}