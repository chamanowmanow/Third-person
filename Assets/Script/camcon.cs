using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camcon : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 3.0f;

    private float rotionX;
    private float rotionY;

    [SerializeField] private Transform target;
    [SerializeField] private float distanceFromtarget = 3.0f;

    private Vector2 crrentRotation;
    private Vector2 smoothVelocity = Vector2.zero;

    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private Vector2 rotationXMinMax = new Vector2 (-20, 40); 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotionX -= mouseY;
        rotionY += mouseX;

        rotionX = Mathf.Clamp(rotionX, rotationXMinMax.x, rotationXMinMax.y);

        Vector2 nexRotaion = new Vector2(rotionX, rotionY);

        crrentRotation = Vector2.SmoothDamp(crrentRotation, nexRotaion, ref smoothVelocity, smoothTime);

        transform.localEulerAngles = crrentRotation;

        transform.position = target.position - transform.forward * distanceFromtarget;

    }
}
