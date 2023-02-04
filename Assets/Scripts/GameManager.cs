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
    [SerializeField] TMP_Text treatText;
    [SerializeField] TMP_Text rocketPackText;
    [SerializeField] Player player;
    [SerializeField] ItemCollection gearCollection;
    [SerializeField] ItemCollection treatCollection;
    [SerializeField] int totalGearsNeeded;
    [SerializeField] int totalRocketPartsNeeded;
    [SerializeField] Stats stats;

    public bool gamePaused = false;
    public GameObject pauseScreen;

    public GameObject instructionsScreen;
    public GameObject instructionPgOne;
    public GameObject instructionPgTwo;
    public GameObject instructionPgThree;

    private void OnEnable()
    {
        HealthBase.OnHealthChanged += UpdateHealth;
        Item.Changed += UpdateInfo;
        StoreClerk.Changed += UpdateInfo;
    }

    private void OnDisable()
    {
        HealthBase.OnHealthChanged -= UpdateHealth;
        Item.Changed -= UpdateInfo;
        StoreClerk.Changed -= UpdateInfo;
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
        treatText.text = treatCollection.amount.ToString();
    }

    void UpdateHealth(GameObject character)
    {
        healthText.text = player.GetHealth().ToString();
    }

    void UpdateInfo()
    {
        gearText.text = gearCollection.amount.ToString();
        treatText.text = treatCollection.amount.ToString();
    }

    public int GetGearsNeeded()
    {
        return totalGearsNeeded;
    }

    public int GetRocketPartsNeeded()
    {
        return totalRocketPartsNeeded;
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
        ResetInstructionScreen();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ResetInstructionScreen()
    {
        instructionPgOne.SetActive(true);
        instructionPgTwo.SetActive(false);
        instructionPgThree.SetActive(false);
        instructionsScreen.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartGameAndBackToMenu()
    {
        Debug.Log("TODO: When Main Menu is ready");
        SceneManager.LoadScene("Main Menu");
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
