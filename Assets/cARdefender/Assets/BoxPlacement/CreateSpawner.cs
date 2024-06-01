using System;
using cARdefender.Tests.BoxPlacement;
using UnityEngine;

namespace cARdefender.Assets.BoxPlacement
{
    public class CreateSpawner : MonoBehaviour
    {

        public BoxConsumer CratePrefab;

        public BoxObtainer boxObtainer;

        public BoxManager boxManager;


        private void Awake()
        {
            boxManager.SubscribeObtainer(boxObtainer);
            boxObtainer.AcceptedTypes.Add(0);
            boxObtainer.OnObtainedBox.AddListener(SpawnCrate);
        }


        public void SpawnCrate(BoxConsumerHandle boxConsumerHandle)
        {
            BoxConsumer boxConsumer = GameObject.Instantiate(CratePrefab);
            boxConsumer.OnBoxLost.AddListener(() =>
            {
                boxManager.Unsubscribe(boxConsumerHandle);
                Destroy(boxConsumer);
            });
            boxConsumer.SubscribeToHandle(boxConsumerHandle);
            
            
        }

    }
}