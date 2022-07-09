using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AI_Director : MonoBehaviour
    {
        private AI_Sensor _sensor;
        private Path _path;

        #region Commands
        private float _commAcc = 1, _commSteer, _commBrake;

        public float CommAcc => _commAcc;
        public float CommSteer => _commSteer;
        public float CommBrake => _commBrake;

        #endregion

        private void Start()
        {
            _sensor = GetComponent<AI_Sensor>();
            _path = GameObject.Find("Path").GetComponent<Path>();
        }
        private void FixedUpdate()
        {
            if (_sensor.ObsDetected)
            {
                PreventCrashes();
            }
            if (_path.ShouldBrake)
            {
                _commBrake = 1;
            }
            else
            {
                _commBrake = 0;
            }
        }


        private void PreventCrashes()
        {
            for (int i = 0; i < _sensor.RayCount; i++)
            {
                if (_sensor.ObsDeltaMags[i] == Mathf.Min(_sensor.ObsDeltaMags))
                {
                    if (_sensor.ObsDeltaMags[i] < _sensor.RayRange)
                    {
                        if (i == 0)
                        {
                            _commBrake = 1;
                            _commSteer = -1;
                        }
                        else if (i == 2)
                        {
                            _commBrake = 1;
                            _commSteer = 1;
                        }
                        else if (i == 1 || i == 4)
                        {
                            _commSteer = 0;
                            _commBrake = 1;
                        }
                        else if (i == 3)
                        {
                            _commBrake = 2;
                            _commSteer = -1;
                        }
                        else if (i == 5)
                        {
                            _commBrake = 2;
                            _commSteer = 1;
                        }
                    }
                }
            }
        }
        public float SteeringForce
        {
            get
            {
                if (_sensor.ObsDetected)
                {
                    return -_commSteer;
                }
                else
                {
                    Vector3 relativeVector = transform.InverseTransformPoint(_path.TargetNode);
                    return relativeVector.normalized.x;
                }
            }
        }
    }
}


