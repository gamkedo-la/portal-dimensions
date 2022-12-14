using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Player : HealthBase
{
    [SerializeField] public AttackRadius attackRadius;
    private Coroutine LookCoroutine;


    private void OnEnable()
    {
        attackRadius.OnAttack += OnAttack;
    }

    private void OnDisable()
    {
        attackRadius.OnAttack -= OnAttack;
    }

    private void OnAttack(IDamageable target)
    {
        //place animation here
        //place attack sound here
        //audioManager.Play(attackSound);

        /*
        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }

        LookCoroutine = StartCoroutine(LookAt(target.GetTransform()));
        */
    }

    private IEnumerator LookAt(Transform target)
    {
        //Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
        Vector3 lookAtPos = target.position;

        float time = 0;

        while (time < 1)
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            lookAtPos.y = transform.position.y;
            transform.LookAt(lookAtPos);

            time += Time.deltaTime * 2;
            yield return null;
        }

        //.rotation = lookRotation;
    }

    private void UpdateVignette()
    {
        //Vignette
    }

    protected override void Killed(GameObject character)
    {
        base.Killed(character);
    }
}
