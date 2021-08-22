using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ComboText : MonoBehaviour
{
    public TextMesh textMesh;
    public int combo;
    private void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }

    private IEnumerator Start()
    {
        textMesh.text = combo.ToString() + "X";
        var renk = textMesh.color;
        textMesh.color = new Color(renk.r, renk.g, renk.b, 1);
        float renkA = renk.a;

        transform.DOJump(new Vector3(transform.position.x + Random.Range(-1f,1f),transform.position.y + 1,transform.position.z),1,1,1);

        yield return new WaitForSeconds(0.75f);
        
        while (textMesh.color.a > 0f)
        {
            renkA -= 0.05f;
            textMesh.color = new Color(renk.r, renk.g, renk.b, renkA);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
