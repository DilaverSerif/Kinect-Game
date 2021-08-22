using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraFollow : MonoBehaviour
{
    private PlayerController player;
    private Rigidbody body;
    private CinemachineCameraOffset offset;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        body = player.gameObject.GetComponent<Rigidbody>();
        offset = GetComponent<CinemachineCameraOffset>();
    }

    private void Update()
    {
        if(player == null) return;

        if (body.velocity.y < 0 & !player.rightFoot & !player.leftFoot)
        {
            offset.m_Offset = Vector3.Lerp(offset.m_Offset,new Vector3(0,-1.5f,0),Time.deltaTime);
        }
        else
        {
            if (offset.m_Offset != Vector3.zero)
            {
                offset.m_Offset = Vector3.Lerp(offset.m_Offset,Vector3.zero, Time.deltaTime);
            }
        }
    }
}
