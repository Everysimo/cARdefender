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
                if (boxConsumer.BoxId == null) continue;
                if(!Boxes.ContainsKey(boxConsumer.BoxId.Value))
                {
                    boxConsumer.LoseBox();
                }
                else
                {
                    OccupiedBoxes.Add(boxConsumer.BoxId.Value);
                }
            }

            
            HashSet<int> freeBoxes = new HashSet<int>(Boxes.Keys);
            freeBoxes.ExceptWith(OccupiedBoxes);
            Queue<int> freeBoxesQueue = new Queue<int>(freeBoxes);
            foreach (BoxConsumer boxConsumer in consumers)
            {
                if(freeBoxesQueue.Count == 0) break;
                if (boxConsumer.BoxId != null) continue;
                int boxId = freeBoxesQueue.Dequeue();
                boxConsumer.ObtainBox(boxId,Boxes[boxId]);
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