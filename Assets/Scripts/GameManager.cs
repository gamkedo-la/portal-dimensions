using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /*
    [SerializeField] public Item coinItem;
    [SerializeField] public Item eggItem;
    [SerializeField] public Item healthItem;
    [SerializeField] public Item powerupItem;
    [SerializeField] public TMP_Text coinDisplayAmount;
    [SerializeField] public TMP_Text eggDisplayAmount;
    [SerializeField] public TMP_Text healthDisplayAmount;
    [SerializeField] public TMP_Text powerupDisplayAmount;
    */

    [SerializeField] List<ItemCollection> item;
    [SerializeField] List<TMP_Text> itemText;
    //[SerializeField] ItemCollection coinItemCollection;
    //[SerializeField] ItemCollection healthItemCollection;
    //[SerializeField] ItemCollection powerupItemCollection;
    //[SerializeField] ItemCollection eggItemCollection;


    private void OnEnable()
    {
        for(int i = 0; i < item.Count; i++)
        {
            item[i].Changed += DisplayHUB;
        }
        
        //item = new List<ItemCollection>();
        //itemText = new List<TMP_Text>();

        /*
        eggItemCollection.Changed += DisplayHUB;
        coinItemCollection.Changed += DisplayHUB;
        healthItemCollection.Changed += DisplayHUB;
        powerupItemCollection.Changed += DisplayHUB;
        */
    }

    private void OnDisable()
    {
        for (int i = 0; i < item.Count; i++)
        {
            item[i].Changed -= DisplayHUB;
        }
        /*
        eggItemCollection.Changed -= DisplayHUB;
        coinItemCollection.Changed -= DisplayHUB;
        healthItemCollection.Changed -= DisplayHUB;
        powerupItemCollection.Changed -= DisplayHUB;
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        DisplayHUB();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        /*
        eggDisplayAmount.SetText(eggItemCollection.Count.ToString());
        coinDisplayAmount.SetText(coinItemCollection.Count.ToString());
        healthDisplayAmount.SetText(healthItemCollection.Count.ToString());
        powerupDisplayAmount.SetText(powerupItemCollection.Count.ToString());
        */

    }
}
