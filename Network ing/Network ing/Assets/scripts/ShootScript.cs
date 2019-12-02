using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootScript : NetworkBehaviour
{
    public float fireRate = 1f;
    public float range = 100f;
        public LayerMask mask;
    private float fireFactor = 0f;
    private GameObject mainCamera;

    //set up fire rates and range
    private void Start()
    {
        Camera camera = GetComponentInChildren<Camera>();
    }

    [Command]
    void Cmd_PlayerShot()
    {

    }


    void HandleInput ()
    {

    }

        private void Update()
    {
        if(isLocalPlayer)
        {
            HandleInput();
        }
    }

}
