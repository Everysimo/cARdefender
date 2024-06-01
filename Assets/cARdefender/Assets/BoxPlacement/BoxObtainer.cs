using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace cARdefender.Tests.BoxPlacement
{
    public class BoxObtainer : MonoBehaviour
    {
        public HashSet<VeichleTypes> AcceptedTypes = new HashSet<VeichleTypes>();
        public UnityEvent<BoxConsumerHandle> OnObtainedBox;
    }
}