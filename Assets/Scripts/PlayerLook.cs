using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEditor;

public class PlayerLook : MonoBehaviour
{
    //public bool disabled = false;

    public Camera cam;
    //private float xRotatio = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public Transform aiOponentTransform; // Pøetažení transformace AI Oponenta do Inspectoru
    public float smoothSpeed = 5.0f; // Plynulost pohybu kamery

    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = transform;
    }
    void Update()
    {
        //isGrounded = controller.isGrounded;

        //if (!disabled)
        //{

        // Nastavte pozici kamery na pozici AI Oponenta s plynulým pohybem
        Vector3 desiredPosition = aiOponentTransform.position;
        Vector3 smoothedPosition = Vector3.Lerp(cameraTransform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        cameraTransform.position = smoothedPosition;

        // Nastavte rotaci kamery tak, aby se dívala stále smìrem k AI Oponentovi
        cameraTransform.LookAt(aiOponentTransform);
        //}
    }

    //public void ProcessLook(Vector2 input)
    //{
    //    float mouseX = input.x;
    //    float mouseY = input.y;
    //    //calculate camera rotation for looking up and down
    //    xRotatio -= (mouseY * Time.deltaTime) * ySensitivity;
    //    xRotatio = Mathf.Clamp(xRotatio, -10f, 42f);
    //    //apply this to our camera transform
    //    cam.transform.localRotation = Quaternion.Euler(xRotatio, 0, 0);
    //    //rotate player to look left and right
    //    transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    //}
}
