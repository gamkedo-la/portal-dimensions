using SoundSystem;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Collection")]
public class ItemCollection : ScriptableObject
{
    public Item item;
    [SerializeField] public string itemName;
    [SerializeField] public int amount;
    [SerializeField] public int worth;
    [SerializeField] public string soundName;
}
