using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMOD_EngineSoundScript : MonoBehaviour
{
    [SerializeField] private CarControl carControl;
    [SerializeField] private StudioEventEmitter[] sounds;

    private float diff;
    private void Start()
    {
       diff =  carControl.maxSpeed/sounds.Length;
    }
    private void Update()
    {
        float speed = Mathf.Floor(Mathf.Abs(Mathf.Clamp(carControl.ForwardSpeed(), 0, carControl.maxSpeed)));

        for (int i = 0; i < sounds.Length; i++)
        {
            if (speed == diff * i) sounds[i].gameObject.SetActive(true);
            else sounds[i].gameObject.SetActive(false);
        }
    }
}
