using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBase : MonoBehaviour
{
    [SerializeField] private Text fps;

    public static AddScore AddScore = new AddScore();
    public static UnityEvent FinishGame = new UnityEvent();
    public static UnityEvent StartGame = new UnityEvent();
    public static ShowScore ShowScore = new ShowScore();

    public ParticlePlaySystem ParticlePlaySystem;
    public SoundSystem SoundSystem;
    public ScoreSystem ScoreSystem;
    public MenuSystem MenuSystem;
    public ComboSystem ComboSystem;
    public SpawnObjectSystem SpawnObjectSystem;

    public GameObject player;
    
    public static GameBase Dilaver;

    private void Awake()
    {
        if (Dilaver == null)
        {
            Dilaver = this;
        }
        else Destroy(gameObject);

        ScoreSystem = gameObject.AddComponent<ScoreSystem>();
        ParticlePlaySystem = gameObject.AddComponent<ParticlePlaySystem>();
        SoundSystem = gameObject.AddComponent<SoundSystem>();
        MenuSystem = gameObject.AddComponent<MenuSystem>();
        ComboSystem = gameObject.AddComponent<ComboSystem>();
        SpawnObjectSystem = gameObject.AddComponent<SpawnObjectSystem>();

        player = FindObjectOfType<PlayerController>().gameObject;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        StartGame.Invoke();
    }

    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    // void OnGUI()
    // {
    //     int w = Screen.width, h = Screen.height;
    //
    //     GUIStyle style = new GUIStyle();
    //
    //     Rect rect = new Rect(0, 0, w, h * 2 / 100);
    //     style.alignment = TextAnchor.UpperLeft;
    //     style.fontSize = h * 2 / 100;
    //     style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
    //     float msec = deltaTime * 1000.0f;
    //     float fps = 1.0f / deltaTime;
    //     string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
    //     GUI.Label(rect, text, style);
    // }
}

public class ScoreSystem : MonoBehaviour
{
    private int totalScore;

    public int TotalScore
    {
        get
        {
            return totalScore;
        }
    }
    
    private void OnEnable()
    {
        GameBase.AddScore.AddListener(AddScore);
    }

    private void AddScore(int score)
    {
        
        if (GameBase.Dilaver.ComboSystem.TotalCombo > 0)
        {
            score += GameBase.Dilaver.ComboSystem.TotalCombo;
        }
        
        totalScore += score;
        GameBase.ShowScore.Invoke(totalScore);
    }

    private void OnDisable()
    {
        GameBase.AddScore.RemoveListener(AddScore);
    }
}


public class AddScore : UnityEvent<int>
{
}

public class ShowScore : UnityEvent<int>
{
}

public class MenuSystem : MonoBehaviour
{
    private Transform gameOverMenu;
    private Text scoreText;
    private Button restartButton, exitButton;

    private void Awake()
    {
        gameOverMenu = transform.Find("GameOverMenu");
        scoreText = transform.Find("Score").Find("Text").GetComponent<Text>();
        scoreText.text = "0";

        restartButton = transform.Find("Restart").GetComponent<Button>();
        restartButton.onClick.AddListener(RestartButton);

        exitButton = transform.Find("Exit").GetComponent<Button>();
        exitButton.onClick.AddListener(ExitButton);
    }

    private void OpenGameOver()
    {
        gameOverMenu.gameObject.SetActive(true);
        //Time.timeScale = 0;
    }

    private void OnEnable()
    {
        GameBase.FinishGame.AddListener(OpenGameOver);
        GameBase.ShowScore.AddListener(ScoreWriter);
    }

    private void OnDisable()
    {
        GameBase.ShowScore.RemoveListener(ScoreWriter);
        GameBase.FinishGame.RemoveListener(OpenGameOver);
    }

    private void ScoreWriter(int score)
    {
        scoreText.text = score.ToString();
    }

    private void RestartButton()
    {
        LoadLevel.levelName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("LoadingScene");
    }

    private void ExitButton()
    {
        LoadLevel.levelName = "MainScene";
        SceneManager.LoadScene("LoadingScene");
    }
}

public class SoundSystem : MonoBehaviour
{
    private List<AudioClip> audioClips = new List<AudioClip>();
    private AudioSource source;
    private float volume = 1f;

    private void Awake()
    {
        Load();
        source = gameObject.AddComponent<AudioSource>();
    }

    private void Load()
    {
        foreach (AudioClip g in Resources.LoadAll("Sounds", typeof(AudioClip)))
        {
            audioClips.Add(g);
        }
    }

    public SoundSystem PlaySound(Sounds sound)
    {
        foreach (var ss in audioClips)
        {
            if (ss.name.ToLower() == sound.ToString().ToLower())
            {
                source.PlayOneShot(ss, volume);
            }
        }

        return this;
    }

    public SoundSystem OverrideVolume(float vol)
    {
        volume = vol;
        return this;
    }
}

public class SpawnObjectSystem : MonoBehaviour
{
    private List<GameObject> gameObjects = new List<GameObject>();

    private void Awake()
    {
        Load();
    }

    private void Load()
    {
        foreach (GameObject g in Resources.LoadAll("GameObjects", typeof(GameObject)))
        {
            gameObjects.Add(g);
        }
    }

    public GameObject GetObject(GameObjects obje, int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            foreach (var g in gameObjects)
            {
                if (obje.ToString().ToLower().Equals(g.name.ToLower()))
                {
                    return g;
                }
            }
        }
        
        Debug.LogError("NOT FOUND GAMEOBJECTS");
        return null;
    }
}

public class ComboSystem : MonoBehaviour
{
    private int totalCombo;
    private bool spawnText = true;
    private GameObject comboText;

    private float time;
    public int TotalCombo
    {
        get { return totalCombo; }
    }

    public ComboSystem AddCombo(int value,Vector3 pos)
    {
        if (value == 0) return this;
        totalCombo += value;
        
        if (spawnText)
        {
            comboText = Instantiate(GameBase.Dilaver.SpawnObjectSystem.GetObject(GameObjects.comboText));
            comboText.transform.position = pos;
            comboText.GetComponent<ComboText>().combo = TotalCombo;
            StartComboCounter();
        }

        return this;
    }

    private IEnumerator ComboCounter()
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        totalCombo = 0;
        
        if (time < 0)
        {
            time = 0;
        }
    }

    private void HardResetCombo()
    {
        StopCoroutine("ComboCounter");
        totalCombo = 0;
    }
    
    private void StartComboCounter()
    {
        if (time == 0)
        {
            time += 7;
            StartCoroutine("ComboCounter");
        }
        else
        {
            time += 3;

            if (time > 10)
            {
                time = 10;
            }
        }
    }

    public ComboSystem ChanceColor(Color color)
    {
        if (comboText == null)
        {
            Debug.LogWarning("ERROR! FIRST USE FUNC ADDCOMBO");
        }
        else comboText.GetComponent<ComboText>().textMesh.color = color;

        return this;
    }
    
    public ComboSystem FailCombo()
    {
        totalCombo = 0;
        return this;
    }

    public ComboSystem SpawnText(bool value)
    {
        spawnText = value;
        return this;
    }
}


public class ParticlePlaySystem : MonoBehaviour
{
    private int amount;
    private Particles particle;
    private Vector3 scale;
    private Vector3 position, offSet;
    private bool loop;
    private bool destory = true;

    private GameObject spawnParticle;
    private List<GameObject> particles = new List<GameObject>();

    private void Awake()
    {
        Load();
    }

    private void Load()
    {
        foreach (GameObject g in Resources.LoadAll("Particles", typeof(GameObject)))
        {
            particles.Add(g);
        }
    }


    public ParticlePlaySystem PlayParticle(Particles particle, Vector3 pos, [Optional] Quaternion rot)
    {
        foreach (var pp in particles)
        {
            if (pp.name.ToLower() == particle.ToString().ToLower())
            {
                if (rot == Quaternion.Euler(0, 0, 0)) rot = Quaternion.identity;
                spawnParticle = Instantiate(pp, pos + offSet, rot);

                if (destory) spawnParticle.gameObject.AddComponent<ParticleDestory>();

                return this;
            }
        }
        Debug.LogWarning("NO FINDING PARTICLE");
        return this;
    }

    public ParticlePlaySystem ChangeScale(Vector3 scale)
    {
        if (spawnParticle == null)
        {
            Debug.LogError("PARTICLE IS NULL FIRST YOU MUST USE FUNC PLAYPARTICLE!");
        }
        else spawnParticle.transform.localScale = scale;
        return this;
    }
    public ParticlePlaySystem SetDestory(bool _value)
    {
        destory = _value;
        return this;
    }

    public ParticlePlaySystem OffSet(Vector3 pos)
    {
        offSet = pos;
        return this;
    }
}


public enum Particles
{
    loot,
    powerUp,
    fail,
    hit,
    castleHit,
    castleDown
}

public enum Sounds
{
    loot,
    hit,
    jump,
    powerUp,
    fail,
    castleHit,
    castleDown
}

public enum GameObjects
{
    comboText
}