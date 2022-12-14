using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text gearText;
    [SerializeField] Player player;
    [SerializeField] ItemCollection gearCollection;
    //private HealthBase playerHealth;

    public bool gamePaused = false;
    public GameObject pauseScreen;

    private void OnEnable()
    {
        gearCollection.amount = 0;
        HealthBase.OnHealthChanged += UpdateHealth;
        Gears.Changed += UpdateGears;
    }

    private void OnDisable()
    {
        HealthBase.OnHealthChanged -= UpdateHealth;
        Gears.Changed -= UpdateGears;
    }

    private void checkPause()
    {
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && !gamePaused)
        {
            PauseGame();
        }
        else if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && gamePaused)
        {
            ResumeGame();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            Debug.LogError("No player set");
        }
        //playerHealth = player;
        DisplayHUB();
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        //********* Debug zone *********
        if (Input.GetKeyDown(KeyCode.Y))
        {
            player.TakeDamage(1);
            Debug.Log("Health is at " + player.GetHealth());
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            player.Heal(1);
            Debug.Log("Health is at " + player.GetHealth());
        }
        checkPause();
    }

    void DisplayHUB()
    {
        healthText.text = player.healthMax.ToString();
        gearText.text = gearCollection.amount.ToString();
    }

    void UpdateHealth(GameObject character)
    {
        healthText.text = player.GetHealth().ToString();
    }

    void UpdateGears()
    {
        gearText.text = gearCollection.amount.ToString();
    }

    void PauseGame()
    {
        Debug.Log("Pausing game");
        Time.timeScale = 0;
        gamePaused = true;
        pauseScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming game");
        Time.timeScale = 1;
        gamePaused = false;
        pauseScreen.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void RestartGameAndBackToMenu()
    {
        Debug.Log("TODO: When Main Menu is ready");

    }
}
