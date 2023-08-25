using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODManager : SingletonMonobehaviour<FMODManager>
{
    [field: SerializeField] public EventReference Music { get; private set; }
    
    [field: Header("UI")]

    [field: SerializeField] public EventReference ButtonPress { get; private set; }
    [field: SerializeField] public EventReference ButtonPressDownBar { get; private set; }

    protected override void Awake()
    {
        base.Awake();

    }
}