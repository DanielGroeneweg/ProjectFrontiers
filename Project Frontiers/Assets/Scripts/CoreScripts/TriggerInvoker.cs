using UnityEngine;
using UnityEngine.Events;
public class TriggerInvoker : MonoBehaviour
{
    public UnityEvent triggerEnter;
    public UnityEvent triggerStay;
    public UnityEvent triggerExit;
    public void OnTriggerEnter(Collider other)
    {
        triggerEnter?.Invoke();
    }

    public void OnTriggerStay(Collider other)
    {
        triggerStay?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        triggerExit?.Invoke();
    }
}