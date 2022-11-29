using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public enum PowerType
    { 
        Run,
        Fly,
        Invuln
    }

    [SerializeField] PowerType powerType;
    [SerializeField] ThirdPersonMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = playerMovement.GetComponent<ThirdPersonMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiatePowerup()
    {
        switch (powerType)
        {
            case PowerType.Run:
                RunBoost();
                Debug.Log("Running");
                break;
            case PowerType.Fly:
                Debug.Log("Flying");
                break;
            case PowerType.Invuln:
                Debug.Log("Invuln");
                break;
            default:
                Debug.Log("No type set!");
                break;
        }
    }

    public void RunBoost()
    {
        playerMovement.UpdateRunBoost();
    }
}
