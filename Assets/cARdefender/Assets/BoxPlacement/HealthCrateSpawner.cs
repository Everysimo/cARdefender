using System;
using cARdefender.Tests.BoxPlacement;
using UnityEngine;

namespace cARdefender.Assets.BoxPlacement
{
    public class HealthCrateSpawner : MonoBehaviour
    {

        public BoxObtainer boxObtainer;
        public BoxManager boxManager;

        public CrateSpawnerView crateSpawnerView;

        private void Awake()
        {
            boxManager.SubscribeObtainer(boxObtainer);
            boxObtainer.AcceptedTypes.Add(VeichleTypes.STOP_SIGN);
            boxObtainer.AcceptedTypes.Add(VeichleTypes.TRAFFIC_LIGHT);
            boxObtainer.OnObtainedBox.AddListener(SpawnCrate);
        }


        public void SpawnCrate(BoxConsumerHandle boxConsumerHandle)
        {
            crateSpawnerView.HealthBoxSpawnRequest(boxConsumerHandle);
        }

    }
}