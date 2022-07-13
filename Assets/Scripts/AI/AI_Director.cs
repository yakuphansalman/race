using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AI_Director : MonoBehaviour
    {
        private AI_Sensor _sensor;
        private AI_Path _path;
        private AI_Physics _physics;
        private void Start()
        {
            _sensor = GetComponent<AI_Sensor>();
            _path = GetComponent<AI_Path>();
            _physics = GetComponent<AI_Physics>();
        }
        private int DirectiveAccelerate()
        {
            return 1;
        }
        private float DirectiveSteer()
        {
            if (_sensor.ObsDetected)
            {
                for (int i = 0; i < _sensor.RayCount; i++)
                {
                    if (i == _sensor.DetectedObsType)
                    {
                        float angularComm = 0.15f;
                        if (i == 0)
                        {
                            return -angularComm;
                        }
                        if (i == 2)
                        {
                            return angularComm;
                        }
                        if (i == 3)
                        {
                            return -angularComm;
                        }
                        if (i == 5)
                        {
                            return angularComm;
                        }
                    }
                }
            }
            Vector3 relativeVector = transform.InverseTransformPoint(_path.TargetNode);
            return relativeVector.normalized.x;
        }
        private float DirectiveBrake()
        {
            if (_sensor.ObsDetected)
            {
                return 0.1f;
            }
            else if (_sensor.OnBrakePoint &&_sensor.BrakePointType != 6)
            {
                Brake_Point brakePoint = _sensor.BrakePoints[_sensor.BrakePointType].GetComponent<Brake_Point>();
                if (_physics.Speed > brakePoint.SpeedLimit)
                {
                    return 1 / _sensor.ForwardDistance;
                }
            }
            return 0;
        }

        public int CommandAccelerate => DirectiveAccelerate();
        public float CommandSteer => DirectiveSteer();
        public float CommandBrake => DirectiveBrake();
    }
}


