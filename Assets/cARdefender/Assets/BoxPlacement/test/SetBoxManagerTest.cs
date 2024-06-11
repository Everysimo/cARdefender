using System;
using System.Collections.Generic;
using cARdefender.Tests.BoxPlacement;
using UnityEngine;

namespace cARdefender.Assets.BoxPlacement.test
{
    public class SetBoxManagerTest : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject[] boxes;
        public BoxManager boxManager;

        private void Awake()
        {
        }

        void Start()
        {
            
            boxManager.UpdateBoxes();
        }

    
    }
}
