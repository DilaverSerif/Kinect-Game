using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FootColliderRight : MonoBehaviour
{
    private PlayerController controller;
    private SphereCollider collider;
    private void Awake()
    {
        controller = FindObjectOfType<PlayerController>();
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            controller.rightFoot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            controller.rightFoot = false;
        }
    }
}