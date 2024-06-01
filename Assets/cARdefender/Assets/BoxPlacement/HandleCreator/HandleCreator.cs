using UnityEngine;

namespace cARdefender.Tests.BoxPlacement.HandleCreator
{
    public abstract class HandleCreator : MonoBehaviour
    {
        public abstract BoxConsumerHandle GetHandle();

    }
}