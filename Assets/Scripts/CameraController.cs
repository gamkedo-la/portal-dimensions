using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followtarget;

    [SerializeField] float rotationSpeed = 5;
    [SerializeField] float distance = 5;

    [SerializeField] float minVertAngle = -45;
    [SerializeField] float maxVertAngle = 45;

    [SerializeField] Vector2 framingOffSet;

    [SerializeField] bool invertX;
    [SerializeField] bool invertY;

    float invertXVal;
    float invertYVal;

    float rotationX;
    float rotationY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        invertXVal = (invertX) ? -1 : 1;
        invertYVal = (invertY) ? -1 : 1;

        rotationX += Input.GetAxis("Mouse Y") * invertYVal * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVertAngle, maxVertAngle);
        rotationY += Input.GetAxis("Mouse X") * invertXVal * rotationSpeed;
        
        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        var focusPosition = followtarget.position + new Vector3(framingOffSet.x, framingOffSet.y, 0);

        transform.position = focusPosition - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;
    }

    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}
