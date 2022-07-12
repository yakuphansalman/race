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
                    if (_sensor.ObsDeltaMags[i] == Mathf.Min(_sensor.ObsDeltaMags))
                    {
                        if (i == 0)
                        {
                            return -1;
                        }
                        if (i == 2)
                        {
                            return 1;
                        }
                        if (i == 1 || i == 4)
                        {
                            return 0;
                        }
                        if (i == 3)
                        {
                            return -1;
                        }
                        if (i == 5)
                        {
                            return 1;
                        }
                    }
                }
            }
            Vector3 relativeVector = transform.InverseTransformPoint(_path.TargetNode);
            return relativeVector.normalized.x;
        }
        private float DirectiveBrake()
        {
            if (_sensor.OnBrakePoint && _physics.Speed > 8f)
            {
                return 1/_sensor.ForwardDistance;
            }
            return 0;
        }

        public int CommandAccelerate => DirectiveAccelerate();
        public float CommandSteer => DirectiveSteer();
        public float CommandBrake => DirectiveBrake();
    }
}


