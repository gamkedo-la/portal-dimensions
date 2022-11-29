using SoundSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemType itemType;
    [SerializeField] MusicEvent pickUpSound;
    public ItemType ItemType => itemType;
    [SerializeField] ItemCollection collection;

    private AudioManager audioManager;
    [SerializeField] string soundName;

    

    private void OnEnable()
    {
        if(GetComponent<MeshRenderer>() != null)
            GetComponent<MeshRenderer>().material.color = itemType.itemColor;
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
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                collection.Add(gameObject.GetComponent<Item>());
            }
            //pickUpSound.Play(2.5f);
            audioManager.Play(soundName);
            Destroy(gameObject);
        }
    }

    private void OnValidate()
    {
        gameObject.name = itemType.name;
    }

}
