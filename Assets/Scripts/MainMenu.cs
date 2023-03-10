using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] ItemCollection gearCollection;
    [SerializeField] ItemCollection treatCollection;
    [SerializeField] Stats stats;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manager found in scene");
        }

        PlayLevelTheme();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Home World Scene");
    }

    public void PlayLevelTheme()
    {
        audioManager.Play("MainTheme");
    }

    public void RestartGame()
    {
        stats.rocketParts = 0;
        gearCollection.amount = 0;
        treatCollection.amount = 0;
    }

    public void Quit()
    {
        Debug.Log("Quit to desktop");
        Application.Quit();
    }
}
