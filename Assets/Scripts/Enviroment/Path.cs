using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Color lineColor;

    private List<Transform> _nodes = new List <Transform>();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = lineColor;

        Transform[] pathTransforms = GetComponentsInChildren<Transform>();
        _nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != transform)
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
            Debug.Log(previousNode + " c= " + currentNode);
        }

    }
}
