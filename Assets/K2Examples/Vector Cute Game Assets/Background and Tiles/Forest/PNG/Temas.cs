using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temas : MonoBehaviour
{
    public bool temas;


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Zemin")
        {
            temas = true;
            //Debug.LogWarning("TEMAS" + gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Zemin")
        {
            temas = false;
        }
    }


    
}
