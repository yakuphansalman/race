using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car Type",menuName = "Car Type")]
public class Car_Preferences_SO : ScriptableObject
{

    #region Controller
    [SerializeField] private float _force;
    [SerializeField] private float _rotationForce;


    public float force => _force;
    public float rotationForce => _rotationForce;
    #endregion

    #region Physics
    [SerializeField] private float _centripitalForceMultiplier;



    public float cMultiplier => _centripitalForceMultiplier;
    #endregion
}
