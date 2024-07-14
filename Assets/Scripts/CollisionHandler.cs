using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentSceneIndex;
    [SerializeField] float loadLevelTime = 1f;
    
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartLevelPassSequence();
                break;
            case "Fuel":
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        // do to: insert crash animation (particle effects)
        GetComponent<Movement>().enabled = false; // Take away player control upon crash
        Invoke("ReloadLevel", loadLevelTime);
    }

    void StartLevelPassSequence()
    {
        // do to: insert level pass animation
        GetComponent<Movement>().enabled = false; // Take away player control upon level completion
        Invoke("LoadNextLevel", loadLevelTime);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextLevelIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevelIndex);
    }
}
