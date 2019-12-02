using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LookScript : NetworkBehaviour
{

    public float mouseSensitivity = 2.0f;
    public float minimumY = -90f;
    public float maximumY = 90f;
    private float yaw = 0f;
    private float pitch = 0f;
    private GameObject mainCamera;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Camera cam = GetComponentInChildren<Camera>();
        if (cam != null)
        {
            mainCamera = cam.gameObject;
        }
    }

    void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void HandleInput()
    {
        //set yaw to rotationy 
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            HandleInput();

            //  yaw = Camera(rotation.Y + MouseX * mouseSensitivity)

            if (Input.GetMouseButton(0))
            {
                pitch += mouseSensitivity * Input.GetAxis("Mouse Y");
                yaw += mouseSensitivity * Input.GetAxis("Mouse X");

                // Clamp pitch:
                pitch = Mathf.Clamp(pitch, -90f, 90f);

                // Wrap yaw:

                while (yaw < 0f)
                {
                    yaw += 360f;
                }
                while (yaw >= 360f)
                {
                    yaw -= 360f;
                }


                // Set orientation:
                transform.eulerAngles = new Vector3(-pitch, yaw, 0f);
            }

        }
    }

    void LateUpdate()
    {
        if(isLocalPlayer)
        {
            mainCamera.transform.localEulerAngles = new Vector3(-pitch, 0, 0);
        }
    }

}
