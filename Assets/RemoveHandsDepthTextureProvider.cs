using System.Collections;
using System.Collections.Generic;
using Meta.XR.Depth;
using UnityEngine;

public class RemoveHandsDepthTextureProvider : MonoBehaviour
{
    [SerializeField]
    private EnvironmentDepthTextureProvider _depthTextureProvider;

    private void Awake()
    {
        // remove hands from depth map
        _depthTextureProvider.RemoveHands(true);

        // restore hands in depth map
        _depthTextureProvider.RemoveHands(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
