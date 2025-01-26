using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMOD_EngineSoundScript : MonoBehaviour
{
    [SerializeField] private CarControl carControl;
    public StudioEventEmitter emitter;

    private void Update()
    {
        foreach (ParamRef param in emitter.Params)
        {
            if (param.Name == "RPM_Or_Sum")
            {
                param.Value = Mathf.Clamp(Mathf.Abs(carControl.ForwardSpeed()), 0, 20);
            }
        }
    }
}
