using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AI_Controller : MonoBehaviour
    {
        [SerializeField] private WheelCollider[] _wheels;
        [SerializeField] private AI_Preferences_SO _aiPrefs;
        [SerializeField] private AI_Sensor _sensor;

        #region Variables


        #endregion

        #region Encapsulated Variables


        #endregion

        private void Update()
        {
            foreach (var wheelCollider in _wheels)
            {
                Accelerate(wheelCollider);
                Steer(wheelCollider);
                Brake(wheelCollider);
            }
        }

        private void Accelerate(WheelCollider wheel)
        {
            if (AI_Manager.Instance.CommAcc != 0 && AI_Physics.Instance.Speed < _aiPrefs.ForceLimit/10)
            {
                wheel.motorTorque = _aiPrefs.Force * AI_Manager.Instance.CommAcc;
            }
            else
            {
                wheel.motorTorque = 0;
            }
        }

        private void Steer(WheelCollider wheel)
        {
            for (int i = 0; i < 2; i++)
            {
                if (wheel == _wheels[i])
                {
                    wheel.steerAngle = _aiPrefs.AngularForce * AI_Manager.Instance.SteeringForce / Mathf.Pow(1.9f, Mathf.Sqrt(AI_Physics.Instance.Speed));
                }
            }
        }
        private void Brake(WheelCollider wheel)
        {
            if (AI_Manager.Instance.CommBrake > 0)
            {
                wheel.brakeTorque = _aiPrefs.BrakeForce * AI_Manager.Instance.CommBrake;
            }
            else
            {
                wheel.brakeTorque = 0;
            }
        }
    }
}

