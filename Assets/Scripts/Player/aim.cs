using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aim : MonoBehaviour
{
    // Start is called before the first frame update
    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
