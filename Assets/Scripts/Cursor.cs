using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    [SerializeField]  GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    [SerializeField] RectTransform canvasRect;

    private Image bar;

    private void Awake()
    {
        bar = transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the game object
        m_PointerEventData.position = this.transform.GetComponent<RectTransform>().position;
 
        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();
 
        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (results.Count > 0)
        {
            if (bar.fillAmount >= 0.99f)
            {
                results[0].gameObject.GetComponent<TouchButton>().load();
            }
        }
    }
}
