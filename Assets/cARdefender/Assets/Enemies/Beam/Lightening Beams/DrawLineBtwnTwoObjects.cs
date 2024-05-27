using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineBtwnTwoObjects : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private LineRenderer LineRenderer;
    [SerializeField] private GameObject StartObject;
    [SerializeField] private GameObject TargetObject;
    
    void Start()
    {
        LineRenderer.positionCount = 2;
        
        LineRenderer.SetPosition(0,StartObject.transform.position);
        LineRenderer.SetPosition(1,TargetObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
