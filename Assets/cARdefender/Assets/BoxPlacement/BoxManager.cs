using System.Collections.Generic;
using UnityEngine;

namespace cARdefender.Tests.BoxPlacement
{
    public class BoxManager : MonoBehaviour
    {
        public Dictionary<int, GameObject> Boxes;
        public HashSet<BoxConsumer> consumers = new HashSet<BoxConsumer>();

       
        public void UpdateBoxes()
        {
            if(Boxes ==  null)return;
            HashSet<int> OccupiedBoxes = new HashSet<int>();
            //detect if a box of a consumer now does not exit anymore
            foreach (BoxConsumer boxConsumer in consumers)
            {
                if (boxConsumer.boxInformation == null) continue;
                if(!Boxes.ContainsKey(boxConsumer.boxInformation.Value.Id))
                {
                    boxConsumer.LoseBox();
                }
                else
                {
                    OccupiedBoxes.Add(boxConsumer.boxInformation.Value.Id);
                }
            }

            
            HashSet<int> freeBoxes = new HashSet<int>(Boxes.Keys);
            freeBoxes.ExceptWith(OccupiedBoxes);
            Queue<int> freeBoxesQueue = new Queue<int>(freeBoxes);
            foreach (BoxConsumer boxConsumer in consumers)
            {
                if(freeBoxesQueue.Count == 0) break;
                if (boxConsumer.boxInformation != null) continue;
                int boxId = freeBoxesQueue.Dequeue();
                boxConsumer.ObtainBox(Boxes[boxId].GetComponent<BoxInformationContainer>().boxInformation);
            }
            
            
        }

        public void Subscribe(BoxConsumer boxConsumer)
        {
            
            consumers.Add(boxConsumer);
            UpdateBoxes();
        }

        public void Unsubscribe(BoxConsumer boxConsumer)
        {
            consumers.Remove(boxConsumer);
            UpdateBoxes();
        }
        
        



    }
}