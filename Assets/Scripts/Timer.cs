using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timeText;
    private int time;

    private void Awake()
    {
        timeText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        GameBase.StartGame.AddListener(() => StartCoroutine("Starting"));
    }

    private void OnDisable()
    {
        GameBase.StartGame.RemoveListener(() => StartCoroutine("Starting"));
        
    }

    private IEnumerator Starting()
    {
        time = 180;

        while (time > 0)
        {
            time -= 1;
            timeText.text = time.ToString();
            yield return new WaitForSeconds(1f);
        }

        GameBase.FinishGame.Invoke();
    }
}