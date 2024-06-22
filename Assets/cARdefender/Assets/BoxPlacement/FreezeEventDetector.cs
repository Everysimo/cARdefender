using System;
using cARdefender.Tests.BoxPlacement;
using UnityEngine;

namespace cARdefender.Assets.BoxPlacement
{
    public class FreezeEventDetector : MonoBehaviour
    {

        public BoxObtainer boxObtainer;
        public BoxManager boxManager;

        public GameFreezerView gameFreezerView;

        private void Awake()
        {
            boxManager.SubscribeObtainer(boxObtainer);
            boxObtainer.AcceptedTypes.Add(VeichleTypes.STOP_SIGN);
            boxObtainer.AcceptedTypes.Add(VeichleTypes.TRAFFIC_LIGHT);
            boxObtainer.OnObtainedBox.AddListener(TryFreezeGame);
        }


        public void TryFreezeGame(BoxConsumerHandle boxConsumerHandle)
        {
            boxManager.consumers.Remove(boxConsumerHandle);
            gameFreezerView.OnGameFreezeRequest();
        }

    }
}