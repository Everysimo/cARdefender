using System;
using UnityEngine;

namespace cARdefender.Tests.RemoteBoundingBoxs
{
    public class SizeTest : MonoBehaviour
    {
        private ObjectRecognized[] recognized = new ObjectRecognized[10];
        private byte[] test;
        private void Start()
        {
            Test();
            
        }

        public void Test()
        {
            unsafe
            {
                
                Debug.Log($"size of ObjectRecognized is {sizeof(ObjectRecognized)}");
            }
        }
    }
}