using System;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    private Button closeButton;

    private void Awake()
    {
        closeButton = GetComponent<Button>();
        closeButton.onClick.AddListener(load);
    }

    public void load()
    {
        Application.Quit();
    }
    
}