using System;
using UnityEngine;

public class Gears : MonoBehaviour
{
    public static event Action Changed;

    private AudioManager audioManager;
    [SerializeField] ItemCollection gear;
    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manager found in scene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            gear.amount += gear.worth;
            audioManager.Play(gear.soundName);
            Changed?.Invoke();
            Destroy(gameObject);
        }
    }
}
