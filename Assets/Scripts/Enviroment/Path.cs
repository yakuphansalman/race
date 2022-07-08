using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private GameObject _car;

    public Color lineColor;

    private List<Transform> _nodes = new List <Transform>();

    private Vector3 _targetNode;

    public Vector3 TargetNode => _targetNode;
    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;

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
            Gizmos.DrawLine(previousNode, currentNode);
            Gizmos.DrawSphere(currentNode, 0.3f);
        }
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
            float nodeRadius = 5f;
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
}
