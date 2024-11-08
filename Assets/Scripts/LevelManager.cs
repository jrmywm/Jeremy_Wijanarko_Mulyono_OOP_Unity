using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] Animator animator;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
        if (animator != null)
        {
            animator.SetTrigger("EndTransition");
            Debug.Log("StartTransition triggered");
        }
        else
        {
            Debug.LogError("Animator not assigned in LevelManager.");
        }

        yield return new WaitForSeconds(1);

        // Pastikan Player.Instance tidak null sebelum mengakses transform
        if (Player.Instance != null)
        {
            Player.Instance.transform.position = new Vector3(0, -4.5f, 0);
        }

        if (animator != null)
        {
            animator.SetTrigger("StartTransition");
            Debug.Log("EndTransition triggered");
        }
        else
        {
            Debug.LogError("Animator not assigned in LevelManager.");
        }
        
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}