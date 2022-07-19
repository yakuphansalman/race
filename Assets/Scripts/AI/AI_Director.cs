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
        private Brake_Point[] _bPoints;
        private void Start()
        {
            _sensor = GetComponent<AI_Sensor>();
            _path = GetComponent<AI_Path>();
            _physics = GetComponent<AI_Physics>();
            _bPoints = GameObject.Find("BrakePoints").GetComponentsInChildren<Brake_Point>();
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
                    float angularComm = 0.5f;
                    if (i == 0 || i == 1)//Right sensors activated
                    {
                        return -angularComm;//Turn Left
                    }
                    else if (i == 2 || i == 3)
                    {
                        return angularComm;//Turn Right
                    }
                    if (i == 0 && i == 2)
                    {
                        return 0;
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
                return 0.5f;
            }
            for (int i = 0; i < _bPoints.Length; i++)
            {
                float delta = (_bPoints[i].gameObject.transform.position - transform.position).magnitude;
                if (delta < _bPoints[i].AreaRadius && _physics.Speed > _bPoints[i].SpeedLimit)
                {
                    return 1f;
                }
            }
            return 0;
        }

        public int CommandAccelerate => DirectiveAccelerate();
        public float CommandSteer => DirectiveSteer();
        public float CommandBrake => DirectiveBrake();
    }
}


