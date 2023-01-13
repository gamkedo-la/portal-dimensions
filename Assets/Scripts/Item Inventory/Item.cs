using SoundSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : PoolableObject
{
    public static event Action<GameObject> Changed;

    [SerializeField] public GameObject item;
    [SerializeField] public string itemName;
    [SerializeField] public int worth;
    [SerializeField] public string soundName;
    [SerializeField] ItemCollection gear;

    private AudioManager audioManager;

    

    private void OnEnable()
    {
        //(GetComponent<MeshRenderer>() != null)
          //  GetComponent<MeshRenderer>().material.color = itemType.itemColor;
    }

    private void Start()
    {
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No audio manager found in scene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            gear.amount += worth;
            audioManager.Play(soundName);
            Changed?.Invoke(item);
            gameObject.SetActive(false);
            /*
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                //collection.Add(gameObject.GetComponent<Item>());
            }
            audioManager.Play(soundName);
            Destroy(gameObject);
            */
        }
    }
}
