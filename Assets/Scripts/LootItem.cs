using UnityEngine;

public abstract class LootItem : MonoBehaviour, ILoot
{
    public int value;
    public Particles particle;
    public Sounds sound;

    public abstract void Take(Transform target);

    public void Effect()
    {
        GameBase.Dilaver.ParticlePlaySystem.PlayParticle(particle,transform.position).ChangeScale(new Vector3(2,2,2));
        GameBase.Dilaver.SoundSystem.PlaySound(sound);
    }

    private bool oneTime;
    private void OnTriggerEnter(Collider other)
    {
        var check = other.transform.root.GetComponent<PlayerController>();
        
        if (check != null & !oneTime)
        {
            oneTime = true;
            Effect();
            Take(other.transform);
        }

    }
}
