using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car Type",menuName = "Car Type")]
public class Car_Preferences_SO : ScriptableObject
{

    #region Controller

    [SerializeField] private float _force;
    [SerializeField] private float _brakeForce;
    [SerializeField] private float _angularForce;
    [SerializeField] private float _forceLimit;


    public float Force => _force;
    public float BrakeForce => _brakeForce;
    public float AngularForce => _angularForce;
    public float ForceLimit => _forceLimit;
    #endregion

    #region Physics

    #endregion

    #region Mechanics
    [SerializeField] private float[] _gearLimits;
    private const float _gearMultiplier = 1.2f;

    public float[] GearLimits => _gearLimits;
    public float M_Gear => _gearMultiplier;
    #endregion

}
