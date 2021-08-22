using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] enemys;
    private Collider2D[] colliders;
    
    
    
    private Transform walkerSpawn;

    private void Awake()
    {
        colliders = GetComponents<Collider2D>();
        walkerSpawn = transform.Find("WalkerSpawn");
    }

    private void OnEnable()
    {
        GameBase.StartGame.AddListener(()=> StartCoroutine("Spawner"));
        GameBase.FinishGame.AddListener(()=> StopCoroutine("Spawner"));
    }

    private void OnDisable()
    {
        GameBase.StartGame.RemoveListener(()=> StartCoroutine("Spawner"));
        GameBase.FinishGame.RemoveListener(()=> StopCoroutine("Spawner"));
    }

    private IEnumerator Spawner()
    {
        while (true)
        {
            for (int i = 0; i < Random.Range(1,6); i++)
            {
                var random = Random.Range(0, enemys.Length);
                
                if (enemys[random].GetComponent<Enemy>().type == EnemyType.flying)
                {
                    Instantiate(enemys[random],
                        RandomPointInBounds(colliders[Random.Range(0, colliders.Length)].bounds), Quaternion.identity);
                }
                else
                {
                    Instantiate(enemys[random],
                        walkerSpawn.position, Quaternion.identity);
                }
                
            }
            
            yield return new WaitForSeconds(Random.Range(3, 10));
        }
    }
    
    
    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
