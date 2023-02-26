using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Bullet
{
    public AnimationCurve positionCurve;
    public AnimationCurve noiseCurve;
    public float yOffset = 1f;
    public Vector2 minNoise = new Vector2(-3f, -0.25f);
    public Vector2 maxNoise = new Vector2(3f, 1f);

    public Coroutine HomingCoroutine;

    public override void Spawn(Vector3 forward, int damage, Transform target)
    {
        this.damage = damage;
        this.target = target;

        if(HomingCoroutine != null)
        {
            StopCoroutine(HomingCoroutine);
        }

        if(target != null)
            HomingCoroutine = StartCoroutine(FindTarget());
        else 
            base.Spawn(forward, damage, target);
    }

    private IEnumerator FindTarget()
    {
        Debug.Log(target);
        /*
        if(target == null)
        {
            GameObject randomTarget = new GameObject();
            //Vector3 pos = new Vector3(transform.position.x + Random.value, transform.position.y + Random.value, transform.position.z + 150);
            Vector3 pos = transform. * 5;
            randomTarget.transform.position = pos;
            target = randomTarget.transform;
            Destroy(randomTarget, 5);

            Vector3 startPosition = transform.position;
            Vector2 noise = new Vector2(Random.Range(minNoise.x, maxNoise.x), Random.Range(minNoise.y, maxNoise.y));
            Vector3 bulletDirectionVector = new Vector3(target.position.x, target.position.y + yOffset, target.position.z) - startPosition;
            Vector3 horizontalNoiseVector = Vector3.Cross(bulletDirectionVector, Vector3.up).normalized;

            float noisePosition = 0;
            float time = 0;

            Debug.Log(target);
            while (time < 1)
            {
                noisePosition = noiseCurve.Evaluate(time);

                transform.position = Vector3.Lerp(startPosition, target.position + new Vector3(0, yOffset, 0),
                    positionCurve.Evaluate(time)) + new Vector3(horizontalNoiseVector.x * noisePosition * noise.x,
                    noisePosition * noise.y * noisePosition * horizontalNoiseVector.z * noise.x);
                transform.LookAt(target.position + new Vector3(0, yOffset, 0));

                time += Time.deltaTime * moveSpeed;

                yield return null;
            }
        }
        */
        Vector3 startPosition = transform.position;
        Vector2 noise = new Vector2(Random.Range(minNoise.x, maxNoise.x), Random.Range(minNoise.y, maxNoise.y));
        Vector3 bulletDirectionVector = new Vector3(target.position.x, target.position.y + yOffset, target.position.z) - startPosition;
        Vector3 horizontalNoiseVector = Vector3.Cross(bulletDirectionVector, Vector3.up).normalized;

        float noisePosition = 0;
        float time = 0;

        Debug.Log(target);
        while (time < 1)
        {
            noisePosition = noiseCurve.Evaluate(time);

            transform.position = Vector3.Lerp(startPosition, target.position + new Vector3(0, yOffset, 0),
                positionCurve.Evaluate(time)) + new Vector3(horizontalNoiseVector.x * noisePosition * noise.x,
                noisePosition * noise.y * noisePosition * horizontalNoiseVector.z * noise.x);
            transform.LookAt(target.position + new Vector3(0, yOffset, 0));

            time += Time.deltaTime * moveSpeed;

            yield return null;
        }      
    }
}
