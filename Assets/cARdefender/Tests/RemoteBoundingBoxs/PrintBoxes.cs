using System;
using UnityEngine;

namespace cARdefender.Tests.RemoteBoundingBoxs
{
    public class PrintBoxes : MonoBehaviour
    {
        public BoxListener boxListener;

        private void Update()
        {
            for (int i = 0; i < boxListener.numOfObjectRecognized; i++)
            {
                Debug.Log($"Object[{i}] : {boxListener.ObjectRecognizeds[i]}");
            }
        }
    }
}