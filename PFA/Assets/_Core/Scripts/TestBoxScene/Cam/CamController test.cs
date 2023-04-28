using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControllertest : MonoBehaviour
{
    public float SensX = 400;
    public float SensY = 400;

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Mouvement();
    }

    void Mouvement()
    {
        float _mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensX;
        float _mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensY;

        yRotation += _mouseX;
        xRotation -= _mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
