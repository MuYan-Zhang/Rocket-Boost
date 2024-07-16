using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadLevelTime = 1f;
    [SerializeField] AudioClip crash, success;
    
    AudioSource audioSource;
    
    int currentSceneIndex;
    bool isTransitioning = false;
    
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) {
        if (isTransitioning) { return; }
        
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
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false; // Take away player control upon crash
        audioSource.PlayOneShot(crash);
        Invoke("ReloadLevel", loadLevelTime);
    }

    void StartLevelPassSequence()
    {
        // do to: insert level pass animation
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false; // Take away player control upon level completion
        audioSource.PlayOneShot(success);
        Invoke("LoadNextLevel", loadLevelTime);
        
    }

    void ReloadLevel()
    {;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextLevelIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevelIndex);
    }
}
