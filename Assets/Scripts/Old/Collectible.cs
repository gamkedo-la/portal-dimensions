using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    public enum ItemType
    {
        Egg,
        Health,
        Money,
        Powerup
    }
    [SerializeField] protected string itemName;
    [SerializeField] protected int worth;
    [SerializeField] protected ItemType type;

    protected int amount;
    protected GameObject collectibleGO;
    protected Transform collectibleTransform;
    protected List<ItemType> egg = new List<ItemType>();
    protected List<ItemType> powerup = new List<ItemType>();
    protected int health = 4;
    protected int money = 0;

    private void OnEnable()
    {
        collectibleGO = gameObject;
        collectibleTransform = gameObject.transform;
    }
    protected virtual void Collect(ItemType item)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Die();
        }
    }

    public void Die()
    {
        if(type == ItemType.Egg)
            Collect(ItemType.Egg);
        if (type == ItemType.Health)
            Collect(ItemType.Health);
        if (type == ItemType.Money)
            Collect(ItemType.Money);
        if (type == ItemType.Powerup)
            Collect(ItemType.Powerup);


        Destroy(collectibleGO);

    }

    /*
    protected Collectible(string name, float worthPoints, int worthGold) 
    {
        this.name = name;
        this.worthPoints = worthPoints;
        this.worthGold = worthGold;
    }

    protected string GetName()
    {
        return name;
    }

    protected float GetWorthPoints()
    { 
        return worthPoints;
    }

    protected int GetWorthGold()
    {
        return worthGold;
    }

    protected void SetName(string name)
    {
        this.name = name;
    }

    protected void SetWorthPoints(float worthPoints)
    {
        this.worthPoints = worthPoints;
    }

    protected void SetWorthGold(int worthGold)
    {
        this.worthGold = worthGold;
    }
    */

}
