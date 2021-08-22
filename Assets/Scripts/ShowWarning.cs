using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WarningSystem
{
    public class ShowWarning : MonoBehaviour
    {
        private Text warningText;
        private Transform warningUI;
        private Button close;
        private void Awake()
        {

            warningUI = transform.GetChild(0);
            warningText = warningUI.Find("Text").GetComponent<Text>();
        }

        private void OnEnable()
        {
            Events.showWarningEvent.AddListener(ShowText);
        }

        private void OnDisable()
        {
            Events.showWarningEvent.RemoveListener(ShowText);
        }

        private void ShowText(string text)
        {
            close = transform.GetChild(0).transform.Find("Okay").GetComponent<Button>();
            close.onClick.AddListener(CloseWarning);
            warningText.text = text;
            Time.timeScale = 0;
            if(!warningUI.gameObject.activeSelf) warningUI.gameObject.SetActive(true);
        }

        private void CloseWarning()
        {
            Time.timeScale = 1;
            warningUI.gameObject.SetActive(false);
        }
    }

    public class Events
    {
        public static ShowWarningEvent showWarningEvent = new ShowWarningEvent();
    }

    public class ShowWarningEvent : UnityEvent<string>
    {
    }
}

