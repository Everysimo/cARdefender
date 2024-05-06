using System.Collections;
using System.Collections.Generic;
using cARdefender.Assets.Enemies.Drone.Scripts.MVC;
using TMPro;
using UnityEngine;

public class ReadHealthFromPlayerView : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI healthText;
    public DroneMVC manager;

    // Update is called once per frame
    void Update()
    {
        healthText.text = $"Health: {manager.droneMvcsManager._playerModel.Life.Value}";
    }
}
