
using System.Collections.Generic;
using UnityEngine;

namespace cARdefender.Tests.BoxPlacement
{
    public class BoxManager : MonoBehaviour
    {
        public Dictionary<int, GameObject> Boxes;
        public HashSet<BoxConsumerHandle> consumers = new HashSet<BoxConsumerHandle>();
        public HashSet<BoxObtainer> BoxObtainers = new HashSet<BoxObtainer>();


        public void UpdateBoxes()
        {
            if(Boxes ==  null)return;
            HashSet<int> OccupiedBoxes = new HashSet<int>();
            //detect if a box of a consumer now does not exit anymore
            foreach (BoxConsumerHandle boxHandle in consumers)
            {
                if (boxHandle.BoxInformation == null) continue;
                if(!Boxes.ContainsKey(boxHandle.BoxInformation.Value.Id))
                {
                    boxHandle.BoxLost();
                }
                else
                {
                    OccupiedBoxes.Add(boxHandle.BoxInformation.Value.Id);
                }
            }

            
            HashSet<int> freeBoxes = new HashSet<int>(Boxes.Keys);
            freeBoxes.ExceptWith(OccupiedBoxes);
            Queue<int> freeBoxesQueue = new Queue<int>(freeBoxes);
            while (freeBoxesQueue.Count > 0)
            {
                int boxId = freeBoxesQueue.Dequeue();
                BoxInformation boxInformation = Boxes[boxId].GetComponent<BoxInformationContainer>().boxInformation;
                bool isLeftover = true;
                foreach (BoxConsumerHandle boxConsumerHandle in consumers)
                {
                    if (boxConsumerHandle.BoxInformation != null) continue;
                    if (boxConsumerHandle.AcceptedLabels.Contains((VeichleTypes)boxInformation.label))
                    {
                        isLeftover = false;
                        boxConsumerHandle.ObtainBox(boxInformation);
                        break;
                    }
                }

                foreach (BoxObtainer boxObtainer in BoxObtainers)
                {
                    if (boxObtainer.AcceptedTypes.Contains((VeichleTypes)boxInformation.label))
                    {
                        BoxConsumerHandle boxConsumerHandle = new BoxConsumerHandle();
                        consumers.Add(boxConsumerHandle);
                        boxObtainer.OnObtainedBox.Invoke(boxConsumerHandle);
                        boxConsumerHandle.ObtainBox(boxInformation);
                        break;
                    }
                }
            }
        }

        public void Subscribe(BoxConsumerHandle boxConsumer)
        {
            
            consumers.Add(boxConsumer);
            UpdateBoxes();
        }

        public void Unsubscribe(BoxConsumerHandle boxConsumer)
        {
            consumers.Remove(boxConsumer);
            UpdateBoxes();
        }

        public void SubscribeObtainer(BoxObtainer boxObtainer)
        {
            BoxObtainers.Add(boxObtainer);
        }

        public void UnsubscribeObtainer(BoxObtainer boxObtainer)
        {
            BoxObtainers.Remove(boxObtainer);
        }
        
        



    }
}