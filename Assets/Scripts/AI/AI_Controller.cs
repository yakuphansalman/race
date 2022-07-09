using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AI_Controller : Car_Controller
    {
        private AI_Director _director;

        private void Start()
        {
            _director = GetComponent<AI_Director>();
        }
        protected override float Steer()
        {
            return _carPrefs.AngularForce * _director.SteeringForce / base.AntiSteer(AI_Physics.Instance.Speed);
        }
        protected override float Accelerate()
        {
            if (_director.CommAcc != 0 && AI_Physics.Instance.Speed < _carPrefs.ForceLimit / 10)
            {
                return _carPrefs.Force * _director.CommAcc;
            }
            return 0;
        }
        protected override float Brake()
        {
            if (_director.CommBrake > 0)
            {
                return _carPrefs.BrakeForce * _director.CommBrake;
            }
            return 0;
        }
    }
}

