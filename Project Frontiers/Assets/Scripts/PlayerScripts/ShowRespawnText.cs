using TMPro;
using UnityEngine;
public class ShowRespawnText : MonoBehaviour
{
    [SerializeField] private TMP_Text respawnText;
    [SerializeField] private CarControl carControl;
    [SerializeField] private float timeBeforeShowingText;

    // private
    private bool shouldShow = false;
    private float timer = 0;
    private void Update()
    {
        if (Mathf.Abs(carControl.ForwardSpeed()) <= 0.1) DoTimer();
        else shouldShow = false;


        if (shouldShow)
        {
            if (!respawnText.gameObject.activeSelf) respawnText.gameObject.SetActive(true);
        }

        else if (respawnText.gameObject.activeSelf) respawnText.gameObject.SetActive(false);
    }
    private void DoTimer()
    {
        timer += Time.deltaTime;
        if (timer >= timeBeforeShowingText)
        {
            timer = 0;
            shouldShow = true;
        }
    }
}
