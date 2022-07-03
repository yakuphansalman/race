using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AI_Controller : MonoBehaviour
    {
        [SerializeField] private WheelCollider[] _wheels;
        [SerializeField] private AI_Preferences_SO _aiPrefs;

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
            if (AI_Manager.Instance.CommAcc != 0)
            {
                wheel.motorTorque = _aiPrefs.Force * AI_Manager.Instance.CommAcc;
            }
        }

        private void Steer(WheelCollider wheel)
        {
            for (int i = 0; i < 2; i++)
            {
                if (wheel == _wheels[i])
                {
                    wheel.steerAngle = _aiPrefs.AngularForce * AI_Manager.Instance.CommSteer;
                }
            }
        }
        private void Brake(WheelCollider wheel)
        {
            if (AI_Physics.Instance.Direction > 0 && AI_Manager.Instance.CommBrake != 0)
            {
                wheel.motorTorque = -_aiPrefs.BrakeForce * AI_Manager.Instance.CommBrake;
            }
        }
    }
}

