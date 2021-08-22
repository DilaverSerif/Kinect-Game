using DG.Tweening;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    private Vector3 pos;

    private void Awake()
    {
        pos = transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        var check = other.transform.root.GetComponent<PlayerController>();

        if (check != null)
        {
            DOTween.Kill(transform);
            transform.DOLocalMoveY(-0.35F, 1F).SetId("down");
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        var check = other.transform.root.GetComponent<PlayerController>();

        if (check != null)
        {
            DOTween.Kill(transform);
            transform.DOShakePosition(1F,new Vector3(0,0.2F,0) ,5).SetId("shake").OnComplete(
                ()=> transform.DOLocalMoveY(0, 0.25F).SetId("shake"));
        }
    }
}
