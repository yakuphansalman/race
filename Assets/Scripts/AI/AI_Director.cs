using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AI_Director : MonoBehaviour
    {
        [SerializeField] private AI_Preferences_SO _prefs;
        private AI_Sensor _aiSensor;
        private Car_Sensor _carSensor;
        private AI_Path _path;
        private AI_Physics _physics;
        private Brake_Point[] _bPoints;

        private void Start()
        {
            _aiSensor = GetComponent<AI_Sensor>();
            _carSensor = GetComponent<Car_Sensor>();
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
            if (_aiSensor.ObsDetected)
            {
                for (int i = 0; i < _aiSensor.RayCount; i++)
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
            else if (CommandOvertake)
            {
                for (int i = 0; i < Race_Manager.Instance.CarPlacements.Length; i++)
                {
                    if (this.gameObject == Race_Manager.Instance.CarPlacements[i] && i != 0)
                    {
                        GameObject carFront = Race_Manager.Instance.CarPlacements[i - 1].gameObject;
                        Car_Sensor frontCarSensor = carFront.GetComponent<Car_Sensor>();
                        for (int j = 0; j < 2; j++)
                        {
                            if (frontCarSensor.HorizontalHits[j].collider.gameObject != null)
                            {
                                float rightDelta = (frontCarSensor.HorizontalHits[0].collider.gameObject.transform.position - transform.position).magnitude;
                                float leftDelta = (frontCarSensor.HorizontalHits[1].collider.gameObject.transform.position - transform.position).magnitude;
                                if (leftDelta < rightDelta)
                                {
                                    return 1;
                                }
                                return -1;
                            }
                        }
                    }
                }
            }
            Vector3 relativeVector = transform.InverseTransformPoint(_path.TargetNode);
            return relativeVector.normalized.x;
        }
        private float DirectiveBrake()
        {
            for (int i = 0; i < Race_Manager.Instance.CarPlacements.Length; i++)
            {
                if (this.gameObject == Race_Manager.Instance.CarPlacements[i] && i != 0)
                {
                    GameObject carFront = Race_Manager.Instance.CarPlacements[i - 1].gameObject;

                    float delta = (carFront.transform.position - transform.position).magnitude;
                    float range = 2f;
                    if (delta < range)
                    {
                        return 1f;
                    }
                }
            }
            if (_aiSensor.ObsDetected)
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
        private bool DirectiveOvertake()
        {
            for (int i = 0; i < Race_Manager.Instance.CarPlacements.Length; i++)
            {
                if (this.gameObject == Race_Manager.Instance.CarPlacements[i] && i != 0)
                {
                    float delta = (Race_Manager.Instance.CarPlacements[i -1].transform.position - transform.position).magnitude;
                    if (delta < _aiSensor.OvertakeRange)
                    {
                        for (int j = 0; j < _carSensor.VerticalHits.Length; j++)
                        {
                            if (_carSensor.VerticalHits[j].collider != null)
                            {
                                if (_carSensor.VerticalHits[j].collider.tag == "Car_Collider")
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        private bool DirectiveSlipstream()
        {
            for (int i = 0; i < Race_Manager.Instance.CarPlacements.Length; i++)
            {
                if (this.gameObject == Race_Manager.Instance.CarPlacements[i] && i != 0)
                {
                    Vector3 relativeVector = this.gameObject.transform.InverseTransformPoint(Race_Manager.Instance.CarPlacements[i - 1].transform.position);
                    if (relativeVector.z < _prefs.SlipstreamRange)
                    {
                        for (int j = 0; j < _carSensor.VerticalHits.Length; j++)
                        {
                            if (_carSensor.VerticalHits[j].collider != null)
                            {
                                if (_carSensor.VerticalHits[j].collider.tag == "Car_Collider")
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public int CommandAccelerate => DirectiveAccelerate();
        public float CommandSteer => DirectiveSteer();
        public float CommandBrake => DirectiveBrake();
        public bool CommandOvertake => DirectiveOvertake();
        public bool CommandSlipstream => DirectiveSlipstream();
    }
}


