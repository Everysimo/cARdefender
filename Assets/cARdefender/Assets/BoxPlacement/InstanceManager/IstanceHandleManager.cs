using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace cARdefender.Tests.BoxPlacement.HandleCreator
{
    public class InstanceHandleManager : MonoBehaviour
    {
        public BoxConsumer boxConsumer;
        public HandleCreator handleCreator;
        private BoxManager boxManager;
        private BoxConsumerHandle boxConsumerHandle;

        private void Awake()
        {
            boxManager = FindObjectOfType<BoxManager>();
            boxConsumerHandle = handleCreator.GetHandle();
            boxConsumer.SubscribeToHandle(boxConsumerHandle);
        }

        private void OnEnable()
        {
            boxManager.Subscribe(boxConsumerHandle);
        }

        private void OnDisable()
        {
            boxManager.Unsubscribe(boxConsumerHandle);
        }
    }
}