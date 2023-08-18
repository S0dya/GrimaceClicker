using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODManager : SingletonMonobehaviour<FMODManager>
{
    [field: Header("Ambience")]

    [field: SerializeField] public EventReference Ambience { get; private set; }
    [field: SerializeField] public EventReference Rain { get; private set; }

    [field: Header("Music")]

    [field: SerializeField] public EventReference Music { get; private set; }
    
    [field: Header("UI")]

    [field: SerializeField] public EventReference ButtonPress { get; private set; }
    [field: SerializeField] public EventReference ButtonPressDownBar { get; private set; }

    protected override void Awake()
    {
        base.Awake();

    }
}