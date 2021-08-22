using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] platforms;

    [SerializeField] private Transform player;
    [SerializeField]
    private float pos, platformY = 3.23f;


    [SerializeField] private int plusPosY = 7;
    private Queue<Transform> spawnList = new Queue<Transform>();
    
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
        pos = player.transform.position.y;
    }

    private void Start()
    {
        Spawn();
        StartCoroutine("PlatformSpawn");
    }

    private IEnumerator PlatformSpawn()
    {
        while (true)
        {
            if (player.transform.position.y > pos)
            {
                pos += 10;
                Spawn();
            }


            yield return new WaitForEndOfFrame();
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < 2; i++)
        {
            var a = Instantiate(platforms[Random.Range(0, platforms.Length)], new Vector3(0, platformY, 0),
                Quaternion.identity);
            spawnList.Enqueue(a);

            platformY += plusPosY;
        }
        
        
        // foreach (var item in spawnList.ToList())
        // {
        //     if (Vector3.Distance(item.position, player.position) > 10 & item.position.y < player.transform.position.y)
        //     {
        //         Destroy(spawnList.Dequeue().gameObject);
        //     }
        // }
    }
}