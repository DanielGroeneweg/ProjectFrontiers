using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CarControl))]
public class AudioLooping : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private CarControl carControl;
    [SerializeField] private AudioSource audioClip;

    [Header("Loop Stats")]
    [SerializeField] private float duration = 0.1f;
    [SerializeField] private float buffer = 0.05f;

    private float maxStartLocation;
    private float startLocation;
    private void Start()
    {
        maxStartLocation = audioClip.clip.length - duration - buffer;
        startLocation = 0;
        StartCoroutine(DoAudioLoop());
    }
    private IEnumerator DoAudioLoop()
    {
        // Play audio from startlocation
        audioClip.Play();
        audioClip.time = startLocation;

        // Wait before continuing
        yield return new WaitForSeconds(duration);
        RestartAudio();
    }

    private void RestartAudio()
    {
        // Recalculate startlocation, then play sound again
        StopCoroutine(DoAudioLoop());
        startLocation = maxStartLocation * carControl.SpeedFactor();
        StartCoroutine(DoAudioLoop());
    }

    private void Update()
    {
        Debug.Log(startLocation);
    }
}
