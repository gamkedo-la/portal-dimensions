using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text gearText;
    [SerializeField] Player player;
    [SerializeField] ItemCollection gearCollection;
    //private HealthBase playerHealth;

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

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            Debug.LogError("No player set");
        }
        //playerHealth = player;
        DisplayHUB();
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
}
