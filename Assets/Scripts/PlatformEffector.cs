using System;
using UnityEngine;

public class PlatformEffector : MonoBehaviour
{
    private Transform point;
    private Collider[] colliders;
    public LayerMask mask;
    private bool enter;
    [SerializeField]
    private float pointY = 2f;
    private Collider myCollider;
    private Collider baseCollider;

    [SerializeField] private Vector3 triggerArea;

    private void Awake()
    {
        point = new GameObject().transform;
        point.SetParent(gameObject.transform);
        point.localPosition = new Vector3(0,pointY,0);
        point.name = "PointCheck";

        baseCollider = transform.Find("cimgovde").GetComponent<Collider>();
    }

    private void Update()
    {
        colliders = Physics.OverlapBox(point.position, triggerArea, Quaternion.identity, mask);

        if (colliders.Length <= 0)
        {
           baseCollider.isTrigger = true;
        }
        else 
        {
            if(!enter)baseCollider.isTrigger = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(new Vector3(transform.position.x,transform.position.y + pointY,transform.position.z), triggerArea);
    }

    private void OnTriggerEnter(Collider other)
    {
        var check = other.transform.root.GetComponent<PlayerController>();
        
        if (check != null)
        {
            enter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var check = other
            .transform.root.GetComponent<PlayerController>();

        if (check != null)
        {
            enter = false;
            
            if (colliders.Length > 0)
            {
               baseCollider.isTrigger = false;
            }
            else
            {
                baseCollider.isTrigger = true;
            }
        }
    }
}
