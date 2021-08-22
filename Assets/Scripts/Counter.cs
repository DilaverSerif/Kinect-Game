using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = transform.GetChild(0).GetComponent<Text>();
    }

    private IEnumerator Start()
    {
        for (int i = 3; i > 0; i--)
        {
            text.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        
        gameObject.SetActive(false);
    }
    
}
