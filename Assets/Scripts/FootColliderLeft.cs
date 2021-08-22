using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FootColliderLeft : MonoBehaviour
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
            controller.leftFoot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            controller.leftFoot = false;
        }
    }
}