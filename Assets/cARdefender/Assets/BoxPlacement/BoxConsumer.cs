using UnityEngine;
using UnityEngine.Events;

namespace cARdefender.Tests.BoxPlacement
{
    public class BoxConsumer : MonoBehaviour
    {
        public BoxInformation? boxInformation;
        public UnityEvent<BoxInformation> OnBoxObtained;
        public UnityEvent OnBoxLost;



        public void LoseBox()
        {
            boxInformation = null;
            OnBoxLost.Invoke();
        }

        public void ObtainBox(BoxInformation newBoxInformation)
        {
            this.boxInformation = newBoxInformation;
            OnBoxObtained.Invoke(newBoxInformation);
        }

        public void OnBoxObtainedAddListener(UnityAction<BoxInformation> newAction)
        {
            OnBoxObtained.AddListener(newAction);
            if (boxInformation.HasValue)
            {
                newAction.Invoke(boxInformation.Value);
            }
        }
    }
}