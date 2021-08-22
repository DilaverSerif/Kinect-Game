using System;
using UnityEngine;
using System.Collections;
public class Test : MonoBehaviour
{
    [SerializeField] private SpriteRenderer backGround;

    [SerializeField]
    private Transform solAyak, sagAyak,solDiz,sagDiz;

    [SerializeField] private Transform engeller;

    private bool go;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        go = true;
    }

    private bool gravity;
    private IEnumerator GravityCheck()
    {
        gravity = true;
        yield return new WaitForSeconds(.5f);
        gravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!go) return;
        
        float cameraY = 0;
        var y = KinectManager.Instance
            .GetJointKinectPosition(2, KinectManager.Instance.GetJointIndex(KinectInterop.JointType.SpineBase)).y;
        
        Debug.Log(y);
        if (y != 0)
        {
            solAyak = transform.Find("FootLeftCollider");
            sagAyak = transform.Find("FootRightCollider");

            solDiz = transform.Find("KneeLeftCollider");
            sagDiz = transform.Find("KneeRightCollider");
        }
        
        if (y > 0.201f)
        {
            backGround.size += new Vector2(0,10* y);
            engeller.position += new Vector3(0,-10* y);
            cameraY = 1.5f;
     
            StopCoroutine("GravityCheck");
            StartCoroutine("GravityCheck");
            return;
        }
        
        if (y < 0.201f & solAyak != null & sagAyak != null)
        {
            if (!sagAyak.GetComponent<Temas>().temas & !solAyak.GetComponent<Temas>().temas )
            {
                if (!solDiz.GetComponent<Temas>().temas & !sagDiz.GetComponent<Temas>().temas)
                {
                    if(gravity) return;
                    backGround.size += new Vector2(0, -5)* Time.deltaTime;
                    engeller.position += new Vector3(0, 5) * Time.deltaTime;
                    cameraY = -1.5f;
                }
            }
            
        }
        else
        {
            cameraY = 0;
        }
        
        // var camPos = CameraFollow.Instance.transform.position;
        // CameraFollow.Instance.Offset = new Vector3(camPos.x, cameraY, camPos.z);
    }
}
