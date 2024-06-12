using cARdefender.Tests.BoxPlacement;
using UnityEngine;

namespace cARdefender.Assets.BoxPlacement
{
    public class ZedVisualizerBoxManagerSetter : MonoBehaviour
    {
        public ZED3DObjectVisualizerRemote zed3DObjectVisualizerRemote;
        public BoxManager boxManager;

        private void Start()
        {
            boxManager.Boxes = zed3DObjectVisualizerRemote.liveBBoxes;
            zed3DObjectVisualizerRemote.OnBoundingBoxUpdate.AddListener(boxManager.UpdateBoxes);
        }
    }
}