using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HealthBase health;
    [SerializeField] protected int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        health = new HealthBase(maxHealth, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        HealthBase.OnKilled += Killed;
    }

    private void OnDisable()
    {
        HealthBase.OnKilled -= Killed;
    }

    private void Killed(GameObject character)
    {
        if (character == gameObject)
        {
            Debug.Log(gameObject.name + "Killed");
        }
    }
}
