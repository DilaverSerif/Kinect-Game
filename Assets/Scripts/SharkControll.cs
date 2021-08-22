using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkControll : PlayerController
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            leftFoot = true;
            rightFoot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            leftFoot = false;
            rightFoot = false;
        }
    }
}
