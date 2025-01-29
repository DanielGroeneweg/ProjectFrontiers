using UnityEngine;
using UnityEngine.Events;
public class AnimationTrigger : MonoBehaviour
{
    private bool hasTriggered = false;
    private bool hasDoneDialogue;
    public UnityEvent StartGame;
    public UnityEvent EnableDialogue;
    public void EnableObjectsTrigger()
    {
        if (!hasTriggered) hasTriggered = true;
        else StartGame?.Invoke();
    }

    public void DialogueTrigger()
    {
        if (!hasDoneDialogue)
        {
            hasDoneDialogue = true;
            EnableDialogue?.Invoke();
        }

        else return;
    }
}