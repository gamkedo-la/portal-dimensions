using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] AttackRadius attackRadius;
    private Coroutine LookCoroutine;
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
        //attackRadius.OnAttack += OnAttack;
    }

    private void OnDisable()
    {
        HealthBase.OnKilled -= Killed;
        //attackRadius.OnAttack -= OnAttack;
    }

    private void OnAttack(IDamageable target)
    {
        //place animation here

        if(LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }

        LookCoroutine = StartCoroutine(LookAt(target.GetTransform()));
    }

    private IEnumerator LookAt(Transform target)
    {
        Quaternion  lookRotation = Quaternion.LookRotation(target.position - transform.position);
        float time = 0;

        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);

            time += Time.deltaTime * 2;
            yield return null;
        }

        transform.rotation = lookRotation;
    }

    private void Killed(GameObject character)
    {
        if (character == gameObject)
        {
            Debug.Log(gameObject.name + "Killed");
        }
    }
}
