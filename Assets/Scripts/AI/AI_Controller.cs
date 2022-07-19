using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AI_Controller : Car_Controller
    {
        private AI_Director _director;
        private AI_Physics _physics;
        private AI_Sensor _sensor;

        private void Start()
        {
            _director = GetComponent<AI_Director>();
            _physics = GetComponent<AI_Physics>();
            _sensor = GetComponent<AI_Sensor>();
        }
        protected override float Steer()
        {
            return _carPrefs.AngularForce * _director.CommandSteer / base.AntiSteer(_physics.Speed);
        }
        protected override float Accelerate()
        {
            if (_physics.Speed < _carPrefs.ForceLimit / 10)
            {
                return _carPrefs.Force * _director.CommandAccelerate;
            }
            
            return 0;
        }
        protected override float Brake()
        {
            return _carPrefs.BrakeForce * _director.CommandBrake;
        }
    }
}

