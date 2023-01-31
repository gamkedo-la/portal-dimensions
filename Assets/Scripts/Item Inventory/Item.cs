using SoundSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : PoolableObject
{
    public static event Action Changed;

    [SerializeField] public GameObject item;
    [SerializeField] public string itemName;
    [SerializeField] public int worth;
    [SerializeField] public string soundName;
    [SerializeField] public ItemCollection collection;

    protected AudioManager audioManager;

    private void Start()
    {
        Debug.Log(gameObject.name);
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No audio manager found in scene");
        }
    }

    public virtual void StatsChanged()
    {
        //Debug.Log(gameObject.name);
        audioManager.Play(soundName);
        Changed?.Invoke();
    }
}
