using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "Item Collection")]
public class ItemCollection : ScriptableObject
{
    [SerializeField] public string itemName;
    [SerializeField] public int amount;
    [SerializeField] public int worth;
    [SerializeField] public Sprite artwork;

    List<ItemType> itemsCollected;

    public int Count => itemsCollected.Count;

    public event Action Changed;

    private void OnEnable()
    {
        itemsCollected = new List<ItemType>();
    }

    public void Add(Item item)
    {
        if(item.gameObject.tag != "Powerup" && item.gameObject.tag != "Health")
        {
            itemsCollected.Add(item.ItemType);
            amount += worth;
            Changed?.Invoke();
        }
        else if(item.gameObject.tag == "Health")
        {

        }
        else
        {
            Powerups powerups = item.gameObject.GetComponent<Powerups>();
            powerups.InitiatePowerup();
        }

    }

    public int CountOf(ItemType itemType)
    {
        return itemsCollected.Count(t => t == itemType);
    }
}
