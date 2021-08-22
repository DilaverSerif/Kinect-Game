using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public static string levelName;

    private void Start()
    {
        LoadingScreen.LoadScreen(levelName);
    }
}
