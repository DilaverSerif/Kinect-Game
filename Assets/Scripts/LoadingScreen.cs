using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class LoadingScreen : MonoBehaviour
{
    private static LoadingScreen instance = null;
    private AsyncOperation loadingStats;
    private Text procesText;
    private Slider procesSlider;
    public bool ProcessVarMi;
    private Image loadingLogo;
    private Image[] images;
    public static void LoadScreen(string levelName)
    {
        DOTween.KillAll();
        
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<LoadingScreen>("GameObjects/LoadingScreen")); // Resources klasöründen LoadingEkrani prefab'ını yükle
            DontDestroyOnLoad(instance.gameObject); // Loading ekranı sahneler arası geçişte kaybolmasın
        }

        // Loading ekranını aktifleştir
        instance.gameObject.SetActive(true);

        // Yeni leveli yüklemeye başla
        instance.loadingStats = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);

        // Yeni levelin yüklenmesi tammalansa bile hemen yeni levela geçiş yapma
        instance.loadingStats.allowSceneActivation = false;

    }
    private void OnEnable()
    {
        if (images[0].color.a != 1)
        {
            foreach (var item in images)
            {
                item.color = Color.white;
            }

            procesText.color = Color.white;
            if (loadingLogo != null) loadingLogo.transform.eulerAngles = Vector3.zero;
            if (procesSlider != null) procesSlider.value = 0;
        }

        StartCoroutine("FakeUpdate");
    }

    private void Awake()
    {

        images = (Image[])GameObject.FindObjectsOfType(typeof(Image));

        procesSlider = GameObject.FindObjectOfType<Slider>();

        if (!ProcessVarMi)
        {
            procesSlider.gameObject.SetActive(false);
        }
        else { procesSlider.value = 0; procesSlider.gameObject.SetActive(true); }

        procesText = GameObject.FindObjectOfType<Text>();

        foreach (var item in images)
        {
            item.color = Color.white;

            if (item.name == "Logo")
            {
                loadingLogo = item;
            }
        }

        procesText.color = Color.white;
        procesText.text = "%0";
    }

    IEnumerator FakeUpdate()
    {
        //if (loadingLogo != null) loadingLogo.transform.DORotate(new Vector3(0, 0, 180), 1f).SetLoops(-1, LoopType.Incremental).SetId("logo");
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            if (procesSlider != null) procesSlider.DOValue(loadingStats.progress, 0.15F);

            procesText.DOText("%" + (int)(loadingStats.progress * 100), 0.15F);
            yield return new WaitForSeconds(0.15f);

            if (loadingStats.progress >= 0.9f)
            {
                procesText.DOText("%" + 100, 0.15F);
                procesSlider.value = 1;
                yield return new WaitForSeconds(0.15f);
                break;
            }
        }

        yield return new WaitForSeconds(1.5f);

        foreach (var item in images)
        {
            item.DOFade(0, 0.5F);
        }
        procesText.DOFade(0, 0.5F);

        yield return new WaitForSeconds(0.5f);

        loadingStats.allowSceneActivation = true;
        DOTween.Kill("logo");
        gameObject.SetActive(false);
    }

}
