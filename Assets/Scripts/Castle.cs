using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [SerializeField] private Slider HpBar;
    private int hp = 100;
    [SerializeField] private float damagePower = 5;

    public static TakeDamage TakeDamage = new TakeDamage();

    private void OnEnable()
    {
        TakeDamage.AddListener(_TakeDamage);
    }

    private void OnDisable()
    {
        TakeDamage.RemoveListener(_TakeDamage);
    }

    private bool check;

    private IEnumerator CheckCHp()
    {
        check = true;
        yield return new WaitForSeconds(2f);

        while (check & hp < 100)
        {
            hp += 1;
            HpBar.DOValue(hp, 0.25f).SetId("hpbar");
            yield return new WaitForSeconds(2f);
        }

        check = false;
    }

    private int lastHp;

    private void _TakeDamage(int damage)
    {
        hp -= damage;
        lastHp = hp;
        if (!check) StartCoroutine("CheckCHp");
        else check = false;
        DOTween.Kill("hpbar");
        DOTween.Kill("shake");
        HpBar.DOValue(hp, 0.25f).SetId("hpbar");
        transform.DOShakeScale(0.2f, damagePower, 5).SetId("shake");

        if (hp <= 0)
        {
            StopCoroutine("CheckCHp");

            GameBase.Dilaver.SoundSystem.PlaySound(Sounds.castleDown);
            hp = 0;
            GetComponent<Collider>().enabled = false;
            GameBase.FinishGame.Invoke();
            GameBase.Dilaver.ParticlePlaySystem.PlayParticle(Particles.castleDown, transform.position);
            transform.DOMoveY(-3, 3F).SetUpdate(true);
        }
    }
}

public class TakeDamage : UnityEvent<int>
{
}