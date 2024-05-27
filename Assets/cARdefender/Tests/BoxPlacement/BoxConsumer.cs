using UnityEngine;
using UnityEngine.Events;

namespace cARdefender.Tests.BoxPlacement
{
    public class BoxConsumer : MonoBehaviour
    {
        public int? BoxId;
        [HideInInspector]
        public GameObject Box;
        public UnityEvent<GameObject> OnBoxObtained;
        public UnityEvent OnBoxLost;

        public void LoseBox()
        {
            BoxId = null;
            Box = null;
            OnBoxLost.Invoke();
        }

        public void ObtainBox(int boxId, GameObject box)
        {
            BoxId = boxId;
            Box = box;
            OnBoxObtained.Invoke(box);
        }
    }
}