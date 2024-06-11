using UnityEngine;
using UnityEngine.Events;

namespace cARdefender.Tests.BoxPlacement
{
    public class BoxConsumer : MonoBehaviour
    {
        
        public UnityEvent<BoxInformation> OnBoxObtained;
        public UnityEvent OnBoxLost;

        public BoxConsumerHandle ConsumerHandle = null;

        public BoxInformation? GetBoxInformation()
        {
            return ConsumerHandle?.BoxInformation;
        }

        public void SubscribeToHandle(BoxConsumerHandle boxConsumerHandle)
        {
            ConsumerHandle?.Unsubscribe(this);

            boxConsumerHandle.Subscribe(this);
            ConsumerHandle = boxConsumerHandle;
            if (boxConsumerHandle.BoxInformation.HasValue)
            {
                OnBoxObtained.Invoke(boxConsumerHandle.BoxInformation.Value);
            }
        }

        public void UnsubscribeToHandle()
        {
            if (ConsumerHandle != null)
            {
                ConsumerHandle.Unsubscribe(this);
                ConsumerHandle = null;
            }
        }

        

        

        public void OnBoxObtainedAddListener(UnityAction<BoxInformation> newAction)
        {
            OnBoxObtained.AddListener(newAction);
            if (ConsumerHandle != null && ConsumerHandle.BoxInformation.HasValue)
            {
                newAction.Invoke(ConsumerHandle.BoxInformation.Value);
            }
        }
    }
}