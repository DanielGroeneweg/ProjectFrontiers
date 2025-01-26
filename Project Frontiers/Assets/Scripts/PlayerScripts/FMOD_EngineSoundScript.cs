using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMOD_EngineSoundScript : MonoBehaviour
{
    [SerializeField] private CarControl carControl;
    [SerializeField] private StudioEventEmitter emitter;
    [SerializeField] private string parameterName;

    private void Update()
    {
        foreach (ParamRef param in emitter.Params)
        {
            if (param.Name == parameterName)
            {
                param.Value = Mathf.Clamp(Mathf.Abs(carControl.ForwardSpeed()), 0, 20);
            }
        }
    }
}
