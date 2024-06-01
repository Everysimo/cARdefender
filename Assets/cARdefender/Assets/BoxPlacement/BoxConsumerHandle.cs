using System.Collections.Generic;

namespace cARdefender.Tests.BoxPlacement
{
    public class BoxConsumerHandle
    {

        public HashSet<BoxConsumer> BoxConsumers;
        public BoxInformation? BoxInformation;
        public HashSet<VeichleTypes> AcceptedLabels;

        public BoxConsumerHandle(HashSet<VeichleTypes> acceptedLabels)
        {
            AcceptedLabels = acceptedLabels;
            BoxConsumers = new HashSet<BoxConsumer>();
        }

        public BoxConsumerHandle()
        {
            AcceptedLabels = new HashSet<VeichleTypes>();
            BoxConsumers = new HashSet<BoxConsumer>();
        }

        public void ObtainBox(BoxInformation boxInformation)
        {
            BoxInformation = boxInformation;
            foreach (BoxConsumer boxConsumer in BoxConsumers)
            {
                boxConsumer.OnBoxObtained.Invoke(boxInformation);
            }
        }

        public void BoxLost()
        {
            BoxInformation = null;
            foreach (BoxConsumer boxConsumer in BoxConsumers)
            {
                boxConsumer.OnBoxLost.Invoke();
            }
        }

        

        public void Subscribe(BoxConsumer boxConsumer)
        {
            BoxConsumers.Add(boxConsumer);
        }

        public void Unsubscribe(BoxConsumer boxConsumer)
        {
            BoxConsumers.Remove(boxConsumer);
        }

        
        
        
    }
}