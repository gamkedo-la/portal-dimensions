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
    }
}
