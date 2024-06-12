using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InvokeEveryXSeconds : MonoBehaviour
{
    // Start is called before the first frame update
    //public UnityEvent invokeEveryXSeconds;
    
    public UnityEvent invokeOnStart;
    public float secondsToWait = 1;
    void Start()
    {
        StartCoroutine(ExecuteOnStartAfrerXSeconds(secondsToWait));
    }

    public IEnumerator ExecuteEveryXSeconds(float seconds)
    {
        while (true)
        {
            //invokeEveryXSeconds?.Invoke();
            yield return new WaitForSeconds(seconds); 
        }
    }

    public IEnumerator ExecuteOnStartAfrerXSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds); 
        invokeOnStart?.Invoke();
    }

    
}
