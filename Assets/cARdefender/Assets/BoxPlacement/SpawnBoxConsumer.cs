using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Tests.BoxPlacement;
using UnityEngine;

public class SpawnBoxConsumer : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxConsumer boxConsumerPrefab;
    
    private BoxConsumer spawnedConsumer;

    public BoxConsumer boxConsumer;

    private void Awake()
    {
        boxConsumer.OnBoxObtainedAddListener(SpawnConsumer);
        boxConsumer.OnBoxLost.AddListener(DestroyConsumer);
    }

    public void SpawnConsumer(BoxInformation boxInformation)
    {
        spawnedConsumer = GameObject.Instantiate(boxConsumerPrefab,transform);
        spawnedConsumer.transform.localPosition = Vector3.zero;
        spawnedConsumer.SubscribeToHandle(boxConsumer.ConsumerHandle);

    }

    public void DestroyConsumer()
    {
        Destroy(spawnedConsumer);
    }

}
