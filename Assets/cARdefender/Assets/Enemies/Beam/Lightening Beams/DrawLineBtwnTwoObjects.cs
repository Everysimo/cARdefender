using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineBtwnTwoObjects : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private LineRenderer LineRenderer;
    [SerializeField] private GameObject StartObject;
    public GameObject TargetObject;
    
    void Start()
    {
        LineRenderer.positionCount = 2;
        
        if (TargetObject != null)
        {
            LineRenderer.enabled = true;
            LineRenderer.SetPosition(0,StartObject.transform.position);
            LineRenderer.SetPosition(1,TargetObject.transform.position);
        }
        else
        {
            LineRenderer.enabled = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetObject != null)
        {
            LineRenderer.enabled = true;
            LineRenderer.SetPosition(0,StartObject.transform.position);
            LineRenderer.SetPosition(1,TargetObject.transform.position);
        }
        else
        {
            LineRenderer.enabled = false;
        }
    }

    public void SetTarget(GameObject target)
    {
        TargetObject = target;
    }

    public void RemoveTarget()
    {
        TargetObject = null;
    }
}
