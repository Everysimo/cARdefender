using System;
using UnityEngine;

namespace cARdefender.Tests.BoxPlacement
{
    public class ZedVisualizerBoxManagerSetter : MonoBehaviour
    {
        public ZED3DObjectVisualizerRemote zed3DObjectVisualizerRemote;
        public BoxManager boxManager;

        private void Awake()
        {
            boxManager.Boxes = zed3DObjectVisualizerRemote.liveBBoxes;
            zed3DObjectVisualizerRemote.OnBoundingBoxUpdate.AddListener(boxManager.UpdateBoxes);
        }
    }
}