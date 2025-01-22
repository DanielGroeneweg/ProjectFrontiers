using System.Collections;
using TMPro;
using UnityEngine;
public class DriftScoreAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text labelText;
    [SerializeField] private float duration;
    [SerializeField] private float moveSpeed;

    private float diff = 0;
    private bool isPlaying = false;
    private float timer = 0;
    public void StartAnimation()
    {
        diff = 1 / duration;
        isPlaying = true;
        StartCoroutine(WaitThenDestroy());
    }

    private void Update()
    {
        if (isPlaying)
        {
            // Change color
            Color color = scoreText.color;
            color.a = 1 - diff * timer;
            scoreText.color = color;
            labelText.color = color;

            // Move text up
            transform.Translate(Vector3.up * moveSpeed * timer);

            timer += Time.deltaTime;
        }
    }

    private IEnumerator WaitThenDestroy()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
