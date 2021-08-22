using System.Collections;
using UnityEngine;

public class ParticleDestory : MonoBehaviour
{
    private float destoryTime;
    private ParticleSystem particle;
    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        destoryTime = particle.duration + particle.startLifetime;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(destoryTime);
        Destroy(gameObject);
    }
}
