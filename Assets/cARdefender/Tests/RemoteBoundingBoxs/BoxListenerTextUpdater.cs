using cARdefender.Tests.RemoteBoundingBoxs;
using TMPro;
using UnityEngine;

public class BoxListenerTextUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI text;
    public BoxListener BoxListener;
   

    void Update()
    {
        text.text = $"Number of boxes is: {BoxListener.numOfObjectRecognized}";
    }
}
