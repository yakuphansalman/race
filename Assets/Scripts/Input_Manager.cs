using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Manager : MonoBehaviour
{

    #region Singleton
    private static Input_Manager instance = null;
    public static Input_Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("SingletonIM").AddComponent<Input_Manager>();
            }
            return instance;
        }
    }
    #endregion

    private float _inputV;
    private float _inputH;

    public float i_Vertical => _inputV;
    public float i_Horizontal => _inputH;
    private void Update()
    {
        _inputV = Input.GetAxis("Vertical");
        _inputH = Input.GetAxis("Horizontal");
    }
}
