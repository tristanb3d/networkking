﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 

public class Synctransform : NetworkBehaviour
{
    public float lerpRate = 15;
    public float positionThreshold = 0.5f;
    public float rotationThreshold = 5.0f;
    private Vector3 lastPosition;
    private Quaternion lastRotation;

    [SyncVar] private Vector3 syncPosition;
    [SyncVar] private Quaternion syncRotation;
    // Start is called before the first frame update

    private Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void LerpPosition()
    {
        if(!isLocalPlayer)
        {
            rigid.position = Vector3.Lerp(rigid.position, syncPosition, Time.deltaTime * lerpRate);
        }

    }

    void LerpRotation()
    {
        if(!isLocalPlayer)
        {
            rigid.rotation = Quaternion.Lerp(rigid.rotation, syncRotation, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdSendPositionToServer(Vector3 _position)
    {
        syncPosition = _position;
        Debug.Log("Position Command");
    }

    [Command]
    void CmdSendRotationToServer(Quaternion _rotation)
    {
        syncRotation = _rotation;
        Debug.Log("Rotation Command");
    }


    [ClientCallback]
    void TransmitPosition()
    {
        if (isLocalPlayer && Vector3.Distance(rigid.position, lastPosition) > positionThreshold)
        {
            CmdSendPositionToServer(rigid.position);
            lastPosition = rigid.position;
        }
    }

    [ClientCallback]
    void TransmitRotation()
    {
        if (isLocalPlayer && Quaternion.Angle(rigid.rotation, lastRotation) > rotationThreshold)
        {
            CmdSendRotationToServer(rigid.rotation);
            lastRotation = rigid.rotation; 
        }
    }


    // Update is called once per frame
    void FixedUpdate ()
    {
        TransmitPosition();
        LerpPosition();
        TransmitRotation();
        LerpRotation();
    }
}
