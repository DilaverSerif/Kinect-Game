using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    private Transform menu;
    private Button closeButton;
    private Text scoreText;
    private Text timeText;

    private void Awake()
    {
        menu = transform.GetChild(0);

        closeButton = menu.transform.Find("Close").GetComponent<Button>();
        closeButton.onClick.AddListener(Close);

        scoreText = menu.transform.Find("ScoreText").GetComponent<Text>();
        timeText = menu.transform.Find("Time").GetComponent<Text>();
    }

    private IEnumerator AutoClose()
    {
        for (int i = 5; i > 0; i--)
        {
            timeText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        
        Close();
    }

    private void Close()
    {
        LoadLevel.levelName = "MainScene";
        SceneManager.LoadScene("LoadingScene");
    }

    private void OpenMenu()
    {
        scoreText.text = GameBase.Dilaver.ScoreSystem.TotalScore.ToString();
        menu.gameObject.SetActive(true);
        StartCoroutine("AutoClose");
    }

    private void OnEnable()
    {
        GameBase.FinishGame.AddListener(OpenMenu);
    }

    private void OnDisable()
    {
        GameBase.FinishGame.RemoveListener(OpenMenu);
    }
}