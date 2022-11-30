using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<ItemCollection> item;
    [SerializeField] List<TMP_Text> itemText;
    [SerializeField] Player player;
    private HealthBase playerHealth;

    private void OnEnable()
    {
        for(int i = 0; i < item.Count; i++)
        {
            item[i].Changed += DisplayHUB;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < item.Count; i++)
        {
            item[i].Changed -= DisplayHUB;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            Debug.LogError("No player set");
        }
        playerHealth = player.health;
        DisplayHUB();
    }

    // Update is called once per frame
    void Update()
    {
        //********* Debug zone *********
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerHealth.Damage(1);
            Debug.Log("Health is at " + playerHealth.GetHealth());
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            playerHealth.Heal(1);
            Debug.Log("Health is at " + playerHealth.GetHealth());
        }
    }

    void DisplayHUB()
    {
        if(item != null)
        {
            for(int i = 0; i < item.Count; i++)
            {
                itemText[i].text = item[i].amount.ToString();
            }
        }
    }
}
