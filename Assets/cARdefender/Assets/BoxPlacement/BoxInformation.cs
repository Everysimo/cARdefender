using UnityEngine;

namespace cARdefender.Tests.BoxPlacement
{
    [System.Serializable]
    public struct BoxInformation
    {
        public int Id;
        public VeichleTypes label;
        public GameObject boxObject;
    }
}