using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car Type",menuName = "Car Type")]
public class Car_Preferences_SO : ScriptableObject
{

    #region Controller
    [SerializeField] private float _force;
    [SerializeField] private float _brakeForce;
    [SerializeField] private float _rotationForce;
    [SerializeField] private float _forceLimit;


    public float Force => _force;
    public float BrakeForce => _brakeForce;
    public float RotationForce => _rotationForce;
    public float ForceLimit => _forceLimit;
    #endregion

    #region Physics

    [SerializeField] private float _centripitalForceMultiplier;
    [SerializeField] private float _friction;

    public float M_Centripital => _centripitalForceMultiplier;
    public float Friction => _friction;

    #endregion

    #region Mechanics
    [SerializeField] private float[] _gearLimits;
    private const float _gearMultiplier = 1.1f;

    public float[] GearLimits => _gearLimits;
    public float M_Gear => _gearMultiplier;
    #endregion

}
