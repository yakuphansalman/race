using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class Player_Controller : Car_Controller
    {
        private Player_Physics _physics;

        private void Start()
        {
            _physics = GetComponent<Player_Physics>();
        }
        protected override float Steer()
        {
            return _carPrefs.AngularForce * Input_Manager.Instance.I_Horizontal / base.AntiSteer(_physics.Speed);
        }
        protected override float Accelerate()
        {
            if (Car_Physics.Instance.Speed < _carPrefs.ForceLimit)
            {
                return _carPrefs.Force * Input_Manager.Instance.I_Vertical;
            }
            return 0;
        }
        protected override float Brake()
        {
            if (Input_Manager.Instance.I_Vertical < 0)
            {
                return _carPrefs.BrakeForce;
            }
            return 0;
        }

        protected override float SlipStreamSpeed()
        {
            return 0;
        }
    }
}

