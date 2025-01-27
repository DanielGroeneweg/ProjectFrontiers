using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMOD_EngineSoundScript : MonoBehaviour
{
    [SerializeField] private CarControl carControl;
    [SerializeField] private StudioEventEmitter[] sounds;
    private void Update()
    {
        float speed = Mathf.Floor(Mathf.Abs(Mathf.Clamp(carControl.ForwardSpeed(), 0, carControl.maxSpeed)));

        Debug.Log(speed);

        if (speed == 0)
        {
            sounds[0].gameObject.SetActive(true);
            return;
        }

        for (int i = 1; i <= sounds.Length; i++)
        {
            if (speed == i) sounds[i-1].gameObject.SetActive(true);
            else sounds[i-1].gameObject.SetActive(false);
        }
    }
}