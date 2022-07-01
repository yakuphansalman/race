using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AI_Controller : AI_Manager
    {
        [SerializeField] private WheelCollider[] _wheels;

        #region Variables


        #endregion

        #region Encapsulated Variables


        #endregion

        private void FixedUpdate()
        {
            foreach (var wheelColl in _wheels)
            {
                Accelerate(wheelColl);
                Steer(wheelColl);
                Brake(wheelColl);
            }
        }

        private void Accelerate(WheelCollider wheel)
        {
            wheel.motorTorque = _aiPrefs.Force * _commAcc;
        }

        private void Steer(WheelCollider wheel)
        {
            wheel.steerAngle = _aiPrefs.AngularForce * _commSteer / Mathf.Pow(_commAcc, Mathf.Sqrt(AI_Physics.Instance.Speed));
        }
        private void Brake(WheelCollider wheel)
        {
            if (AI_Physics.Instance.Direction > 0)
            {
                wheel.motorTorque = -_aiPrefs.BrakeForce * _commBrake;
            }
        }
    }
}

