using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car Type",menuName = "Car Type")]
public class Car_Preferences_SO : ScriptableObject
{

    #region Controller
    [SerializeField] private float _force;
    [SerializeField] private float _rotationForce;
    [SerializeField] private float _forceLimit;


    public float force => _force;
    public float rotationForce => _rotationForce;
    public float forceLimit => _forceLimit;
    #endregion

    #region Physics
    [SerializeField] private float _centripitalForceMultiplier;


    public float cMultiplier => _centripitalForceMultiplier;

    #endregion

    #region Mechanics
    [SerializeField] private float[] _gearLimits;
    private const float _gearMultiplier = 1.1f;

    public float[] a_gearLimits => _gearLimits;
    public float m_gear => _gearMultiplier;
    #endregion
}
