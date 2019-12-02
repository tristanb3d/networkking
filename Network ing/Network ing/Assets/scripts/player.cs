using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class player : NetworkBehaviour
{
    public float movementSpeed = 10.0f;
    public float rotationSpeed = 10.0f;
    public float JumpHeight = 24.0f;
    private bool isGrounded = false;
    private Rigidbody rigid;



    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        AudioListener audioListener = GetComponentInChildren<AudioListener>();
        Camera camera = GetComponentInChildren<Camera>();

        if (isLocalPlayer)
        {
            camera.enabled = true;
            audioListener.enabled = true;
        }
        else;
        {
            camera.enabled = false;
            audioListener.enabled = false;
            AssignRemoteLayer();
        }
        RegisterPlayer();

    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            HandleInput();
        }
    }


    void Move(KeyCode _key)
    {
        Vector3 position = rigid.position;
        Quaternion rotation = rigid.rotation;
        switch (_key)
        {
            case KeyCode.W:
                position += transform.forward * movementSpeed * Time.deltaTime;
                break;
            case KeyCode.S:
                position += -transform.forward * movementSpeed * Time.deltaTime;
                break;
            case KeyCode.A:
                position += -transform.right * movementSpeed * Time.deltaTime;
                break;
            case KeyCode.D:
                position += transform.right * movementSpeed * Time.deltaTime;
                break;
            case KeyCode.Space:
                if (isGrounded)
                {
                    rigid.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
                    isGrounded = false;
                }
                break;
        }
        rigid.MovePosition(position);
        rigid.MoveRotation(rotation);
    }

    void HandleInput()
    {
        KeyCode[] keys =
      {
                KeyCode.W,
                KeyCode.S,
                KeyCode.A,
                KeyCode.D,
                KeyCode.Space
            };

        foreach (var key in keys)
        {
            if (Input.GetKey(key))
            {
                Move(key);
            }
        }

    }

    void OnCollisionEnter(Collision _col)
    {
        isGrounded = true;
    }

    void RegisterPlayer()
    {
        string ID = "Player" + GetComponent<NetworkIdentity>().netId;
        this.name = ID;
    }

    void AssignRemoteLayer()
    {
      //  gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }


}
    

