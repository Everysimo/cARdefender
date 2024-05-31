using System.Collections;
using System.Collections.Generic;
using cARdefender.Tests.BoxPlacement;
using UnityEngine;

public class SetLineTargetOnBoxEvent : MonoBehaviour
{
    public BoxConsumer boxConsumer;

    public DrawLineBtwnTwoObjects drawLineBtwnTwoObjects;
    void Awake()
    {
        boxConsumer.OnBoxLost.AddListener(drawLineBtwnTwoObjects.RemoveTarget);
        boxConsumer.OnBoxObtainedAddListener(SetLineTarget);
    }

    public void SetLineTarget(BoxInformation boxInformation)
    {
        Debug.Log($"{nameof(SetLineTargetOnBoxEvent)}: Setting target on {boxInformation.boxObject.name}");
        drawLineBtwnTwoObjects.SetTarget(boxInformation.boxObject);
    }

}
