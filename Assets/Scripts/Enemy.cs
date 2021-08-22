using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private Transform player;
    private Vector3 lookPos;
    public EnemyType type;

    [SerializeField]
    private int damage,score;
    private void Awake()
    {
        target = FindObjectOfType<Castle>().transform;

        if (type == EnemyType.flying)
        {
            lookPos = new Vector3(target.position.x, Random.Range(1,7), 0);
        }
        else lookPos = new Vector3(target.position.x, transform.position.y, 0);
        
        if (SceneManager.GetActiveScene().name == "SharkBridge")
        {
            if (type == EnemyType.flying)
            {
                lookPos = target.position;
            }
            else lookPos = new Vector3(target.position.x, transform.position.y, 0);
            
        }
        player = FindObjectOfType<PlayerController>().transform;
    }

    private void OnEnable()
    {
        transform.DOLookAt(lookPos,0f);
        
        GameBase.FinishGame.AddListener(()=> gameObject.SetActive(false));

        if (SceneManager.GetActiveScene().name != "SharkBridge")
        {
            if (type == EnemyType.flying)
            {
                transform.DOMove(new Vector3(target.position.x, target.position.y + Random.Range(1,7), 0), Random.Range(15, 25f));
            }
            else
            {
                transform.DOMove(new Vector3(target.position.x, target.position.y, 0), Random.Range(15, 25f));
            }
        }
        else
        {
            if (type == EnemyType.flying)
            {
                transform.DOMove(new Vector3(target.position.x, target.position.y, 0), Random.Range(15, 25f));
            }
            else transform.DOMove(new Vector3(target.position.x, transform.position.y, 0), Random.Range(15, 25f));
        }




    }

    private void OnDisable()
    {
        GameBase.FinishGame.RemoveListener(()=> gameObject.SetActive(false));
    }

    private bool oneShot;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Player" & !oneShot)
        {
            oneShot = true;
            DOTween.Kill(transform);
            var x = transform.position.x - player.transform.position.x;
            GameBase.AddScore.Invoke(score);
            GameBase.Dilaver.SoundSystem.PlaySound(Sounds.hit);
            GameBase.Dilaver.ParticlePlaySystem.PlayParticle(Particles.hit, transform.position);
            GetComponent<Animator>().SetTrigger("damage");
            GameBase.Dilaver.ComboSystem.AddCombo(1,transform.position).ChanceColor(new Color(Random.Range(0,1f),Random.Range(0,1f),Random.Range(0,1f)));
            transform.DOMove(new Vector3(Random.Range(20, 30) * x, Random.Range(20, 30), 0),5f).OnComplete(
                ()=> Destroy(gameObject)
                );
        }
        
        if (other.transform.tag == "Castle")
        {
            DOTween.Kill(transform);
            Castle.TakeDamage.Invoke(damage);
            GameBase.Dilaver.SoundSystem.PlaySound(Sounds.castleHit);
            GameBase.Dilaver.ParticlePlaySystem.PlayParticle(Particles.castleHit, transform.position);
            Destroy(gameObject);
        }
        
    }
    
}

public enum EnemyType
{
    walker,
    flying,
    mid
}