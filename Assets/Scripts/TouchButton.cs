using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TouchButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] private string sceneName;
    private float time;

    private Image barCursor;

    private void Awake()
    {
        barCursor = transform.parent.Find("Cursor").transform.Find("Bar").GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine("TimeCounter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine("TimeCounter");
        time = 0;
        barCursor.fillAmount = 0;
    }

    public void load()
    {
        LoadLevel.levelName = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    
    private IEnumerator TimeCounter()
    {
        while (time < 1)
        {
            time += 0.1f;
            barCursor.fillAmount = time;
            yield return new WaitForSeconds(0.1f);
        }

        load();
        //LoadingScreen.LoadScreen(sceneName);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        load();
    }
}
