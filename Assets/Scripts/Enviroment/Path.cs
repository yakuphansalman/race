using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private GameObject _car;

        private List<Transform> _nodes = new List<Transform>();

        [SerializeField] private float _nodeRadius;

        private Vector3 _targetNode;


        public Vector3 TargetNode => _targetNode;
        private void FixedUpdate()
        {
            SetDestination();
        }
        private void Update()
        {
            DetermineNextNode();
        }
        private void DetermineNextNode()
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                Debug.DrawRay(_car.transform.position, _targetNode, Color.blue);
                float nodeRadius = 25f;
                float delta = (_nodes[i].position - _car.transform.position).magnitude;
                if (delta < nodeRadius)
                {
                    if (i < _nodes.Count - 1)
                    {
                        _targetNode = _nodes[i + 1].position;
                    }
                    else if (i == _nodes.Count - 1)
                    {
                        _targetNode = _nodes[0].position;
                    }
                }
            }
        }
        private bool CheckShouldBrake()
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                float delta = (_nodes[i].position - _car.transform.position).magnitude;
                if (delta < _nodeRadius && i != 0 && AI_Physics.Instance.Speed > 3f)
                {
                    if (!_car.GetComponent<AI_Sensor>().ObsDetected)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool ShouldBrake
        {
            get
            {
                return CheckShouldBrake();
            }
        }
        private void SetDestination()
        {
            Transform[] pathTransforms = GetComponentsInChildren<Transform>();
            _nodes = new List<Transform>();

            for (int i = 0; i < pathTransforms.Length; i++)
            {
                if (pathTransforms[i] != transform && pathTransforms[i].position != Vector3.zero)
                {
                    _nodes.Add(pathTransforms[i]);
                }
            }
            for (int i = 0; i < _nodes.Count; i++)
            {
                Vector3 currentNode = _nodes[i].position;
                Vector3 previousNode = Vector3.zero;
                if (i > 0)
                {
                    previousNode = _nodes[i - 1].position;
                }
                else if (i == 0 && _nodes.Count > 1)
                {
                    previousNode = _nodes[_nodes.Count - 1].position;
                }
            }
        }
    }
}
