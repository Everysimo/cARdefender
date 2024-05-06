using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InvokeEveryXSeconds : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent invokeEveryXSeconds;
    public float secondsToWait = 1;
    void Start()
    {
        StartCoroutine(ExecuteEveryXSeconds(secondsToWait));
    }

    public IEnumerator ExecuteEveryXSeconds(float seconds)
    {
        while (true)
        {
            invokeEveryXSeconds?.Invoke();
            yield return new WaitForSeconds(seconds); 
        }
    }

    
}
