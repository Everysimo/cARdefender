using System;
using UnityEngine;

namespace cARdefender.Tests.BoxPlacement
{
    public class SubscribeLyfeCycle : MonoBehaviour
    {
        public BoxConsumer boxConsumer;

        private BoxManager boxManager;
        private void Awake()
        {
            boxManager = FindObjectOfType<BoxManager>();
        }

        private void OnEnable()
        {
            boxManager.Subscribe(boxConsumer);
        }

        private void OnDisable()
        {
            boxManager.Unsubscribe(boxConsumer);
        }

       
    }
}