using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaceObjectOnARandomBoundingBox : MonoBehaviour
{
    private static HashSet<int> OccupiedBoxes = new HashSet<int>();
    // Start is called before the first frame update
    public Transform objectToMoveTransform;
    public float heightOffset;
    public ZED3DObjectVisualizerRemote zed3DObjectVisualizerRemote;

    private int currentObject = -1;
    
    // Update is called once per frame
    void Update()
    {
        MoveObject();
    }

    private void Awake()
    {
        if (zed3DObjectVisualizerRemote == null)
        {
            zed3DObjectVisualizerRemote = FindObjectOfType<ZED3DObjectVisualizerRemote>();
        }

        if (zed3DObjectVisualizerRemote == null)
        {
            Debug.LogError("Non è stato trovato nessun 3dObjectVisualizer.");

        }
       
        
    }

    public void MoveObject()
    {
        Transform boxTransform = ChooseBox();
        if (boxTransform == null)
        {
            return;
        }
        
        objectToMoveTransform.position =
            boxTransform.position + Vector3.up * (boxTransform.lossyScale.y/2 + heightOffset);
    }

    private Transform ChooseBox()
    {
        if (zed3DObjectVisualizerRemote.liveBBoxes.ContainsKey(currentObject))
        {
            return zed3DObjectVisualizerRemote.liveBBoxes[currentObject].transform;
        }

        if (currentObject >= 0)
        {
            OccupiedBoxes.Remove(currentObject);
            currentObject = -1;
        }

        int count = zed3DObjectVisualizerRemote.liveBBoxes.Count;
        if (count == 0)
        {
            return null;
        }

        HashSet<int> allBoxes = zed3DObjectVisualizerRemote.liveBBoxes.Keys.ToHashSet();
        allBoxes.ExceptWith(OccupiedBoxes);
        count = allBoxes.Count;
        if (count == 0)
        {
            return null;
        }

        int index = Random.Range(0, count);
        currentObject = allBoxes.ToList()[index];
        OccupiedBoxes.Add(currentObject);
        return zed3DObjectVisualizerRemote.liveBBoxes[currentObject].transform;


    }
}
