using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;

namespace cARdefender.Tests.RemoteBoundingBoxs
{
    public class BoxListener : MonoBehaviour
    {
        public const int MaxObjects = 20;

        public ObjectRecognized[] ObjectRecognizeds = new ObjectRecognized[MaxObjects];
        public int numOfObjectRecognized = 0;
        public Vector3 positionOfCamera;
        public Quaternion camRotation;

        public UnityEvent OnNewDetection;


        public Transform camera;
        protected UdpClient Client;
        protected IPEndPoint RemoteIpEndPoint;

 

        private void Start()
        {
           
            Client = new UdpClient(9000);
            RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 9000);
        }

        private void Update()
        {
            ListenForUDPPackages();
        }


        public unsafe void ListenForUDPPackages()
        {
            byte[] bytes = null;
            /*
            the reason for the do while is that if something new has already arrived
            (like when multiple packets arrive at the same time),
            just process the latest packet and ignore the rest
            */
            
            Debug.Log("Listening");
            while (Client.Available > 0)
            {
                bytes = Client.Receive(ref RemoteIpEndPoint);
                Debug.Log($"Received {bytes.Length}");
            }
            if (bytes == null)
            {
                numOfObjectRecognized = 0;
                return;
            }

            if (bytes.Length == 1)
            {
                numOfObjectRecognized = 0;
                positionOfCamera = camera.position;
                camRotation = camera.rotation;
                OnNewDetection.Invoke();
                return;
            }


            int bytesToWrite = Mathf.Min(bytes.Length, MaxObjects * sizeof(ObjectRecognized));
            numOfObjectRecognized = bytesToWrite / sizeof(ObjectRecognized);

            fixed (byte* source = bytes)
            {
                fixed (ObjectRecognized* dest = ObjectRecognizeds)
                {
                    byte* destination = (byte*)dest;
                    for (int i = 0; i < bytesToWrite; i++)
                    {
                        destination[i] = source[i];
                    }
                }
            }

            positionOfCamera = camera.position;
            camRotation = camera.rotation;
            OnNewDetection.Invoke();
        }


        
    }
}