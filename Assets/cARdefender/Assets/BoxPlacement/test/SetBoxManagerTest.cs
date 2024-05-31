using System.Collections;
using System.Collections.Generic;
using cARdefender.Tests.BoxPlacement;
using UnityEngine;

public class SetBoxManagerTest : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] boxes;
    public BoxManager boxManager;
    void Start()
    {
        Dictionary<int, GameObject> dictionary = new Dictionary<int, GameObject>();
        for (int i = 0; i < boxes.Length; i++)
        {
            dictionary.Add(i,boxes[i]);
        }

        boxManager.Boxes = dictionary;
        boxManager.UpdateBoxes();
    }

    
}
