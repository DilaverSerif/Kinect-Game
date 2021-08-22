using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    
    private void OnEnable()
    {
        if(source.playOnAwake) return;
        GameBase.StartGame.AddListener(()=> source.Play());
        GameBase.FinishGame.AddListener(()=> source.Stop());
    }

    private void OnDisable()
    {
        GameBase.StartGame.RemoveListener(()=> source.Play());
        GameBase.FinishGame.RemoveListener(()=> source.Stop());
    }
}
