using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI
{
    public class AI_Path : MonoBehaviour
    {
        private List<Transform> _nodes = new List<Transform>();

        [SerializeField] float _nodeRadius;

        private Vector3 _targetNode;
        private Vector3 _targetDelta;
        public Vector3 TargetNode => _targetNode;
        public Vector3 TargetDelta => _targetDelta;
        private void FixedUpdate()
        {
            SetDestination();
        }
        private void Update()
        {
            DetermineNextNode();
            _targetDelta = _targetNode - transform.position;
        }
        private void DetermineNextNode()
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                float delta = (_nodes[i].position - transform.position).magnitude;
                if (delta < _nodeRadius)
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
        private void SetDestination()
        {
            Transform[] pathTransforms = GameObject.Find("Path").GetComponentsInChildren<Transform>();
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

