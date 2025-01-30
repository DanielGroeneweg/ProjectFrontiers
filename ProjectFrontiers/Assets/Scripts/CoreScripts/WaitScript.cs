using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class WaitScript : MonoBehaviour
{
    public UnityEvent AfterWaiting;
    private float time;
    public void StartWaiting(float secondsToWait)
    {
        time = secondsToWait;
        StartCoroutine("Waiting");
    }
    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(time);
        AfterWaiting?.Invoke();
    }
}
