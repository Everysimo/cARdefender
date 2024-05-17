using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaceObjectOnARandomBoundingBox : MonoBehaviour
{
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

    private void OnEnable()
    {
       
        // Elenco degli oggetti in scena
        GameObject[] objs = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in objs)
        {
            // Controlla se l'oggetto ha lo script Zed3DObjectVisualizerRemote
            ZED3DObjectVisualizerRemote objectVisualizerRemote = obj.GetComponent<ZED3DObjectVisualizerRemote>();
            if (objectVisualizerRemote != null)
            {
                // Ottieni il Transform dell'oggetto trovato
                zed3DObjectVisualizerRemote = objectVisualizerRemote;
                break; // Esci dal loop una volta trovato l'oggetto desiderato
            }
            
            
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

        int count = zed3DObjectVisualizerRemote.liveBBoxes.Count;
        if (count == 0)
        {
            return null;
        }

        int current = Random.Range(0, count);
        currentObject = zed3DObjectVisualizerRemote.liveBBoxes.Keys.ToList()[current];
        return zed3DObjectVisualizerRemote.liveBBoxes[currentObject].transform;


    }
}
