using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : SingletonBase<SpeedManager>
{
    [Header("Status Vars")]
    [SerializeField]
    private float startSpeed; //start speed multiplier

    //aux vars
    private float currentSpeed; //current speed of ships

    public float Speed { get => currentSpeed; }
    protected override void Awake()
    {
        base.Awake();
        currentSpeed = startSpeed;
        InvokeRepeating("attSpeed", 0, 1);//starting method to add speed over time
    }
    private void attSpeed()
    {
        currentSpeed = startSpeed * (1 + (Time.timeSinceLevelLoad * .1f));
    }
}
