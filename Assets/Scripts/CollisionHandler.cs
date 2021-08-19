using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEditor.SearchService.Scene;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
    
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip finishSFX;
    
    [SerializeField]  ParticleSystem crashPS;
    [SerializeField]  ParticleSystem finishPS;
    
    AudioSource _audioSource;


    bool isTransitioning = false;
    private bool collisionDisabled = false;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        DebugKeys();
    }

    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) {return;}
        
        switch (other.gameObject.tag)
        {
            case "Respawn":
                break;
            case "Finish":
                StartFinishSequence();
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        _audioSource.Stop();
        isTransitioning = true;
        _audioSource.PlayOneShot(crashSFX);
        crashPS.Play();
        GetComponent<Mover>().enabled = false;
        Invoke("ReloadLevel",delayTime);
    }
    void StartFinishSequence()
    {
        _audioSource.Stop();
        isTransitioning = true;
        _audioSource.PlayOneShot(finishSFX);
        finishPS.Play();
        GetComponent<Mover>().enabled = false;
        Invoke("LoadNextLevel", delayTime);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    
}
