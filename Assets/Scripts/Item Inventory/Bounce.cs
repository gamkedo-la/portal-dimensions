using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bounce : MonoBehaviour
{
    public float bounceSpeed = 8;
    public float bounceAmplitude = 0.05f;
    public float rotationSpeed = 90;

    private float startingHeight;
    private float timeOffSet;

    [SerializeField] float yOffset;
    [SerializeField] public int groundLayerIndex;


    // Start is called before the first frame update
    void Start()
    {
        //groundLayerIndex = Mathf.RoundToInt(Mathf.Log(groundLayer.value, 2));
        Debug.Log(groundLayerIndex);
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))//, float.MaxValue, groundLayer))
        {
            Debug.Log("GroundLayerIndex: " + groundLayerIndex + " hits layer " + hit.collider.gameObject.layer);
            if (hit.collider.gameObject.layer == groundLayerIndex)
            {
                transform.position = new Vector3(hit.point.x, hit.point.y + yOffset, hit.point.z);
            }
        }

        startingHeight = transform.localPosition.y;
        timeOffSet = Random.value * Mathf.PI * 2;
    }

    // Update is called once per frame
    void Update()
    {
        //bounce effect
        float finalHeight = startingHeight + Mathf.Sin(Time.time * bounceSpeed * timeOffSet) * bounceAmplitude;
        var position = transform.localPosition;
        position.y = finalHeight;
        transform.localPosition = position;

        //spin
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotation.y += rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
}
