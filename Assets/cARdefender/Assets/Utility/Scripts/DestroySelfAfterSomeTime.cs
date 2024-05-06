using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfterSomeTime : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float secondsBeforeDestruction;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(secondsBeforeDestruction);
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}