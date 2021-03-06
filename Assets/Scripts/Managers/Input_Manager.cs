using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Manager : MonoBehaviour
{

    private float _inputV;
    private float _inputH;

    public float I_Vertical => _inputV;
    public float I_Horizontal => _inputH;

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
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion
    private void Update()
    {
        _inputV = Input.GetAxis("Vertical");
        _inputH = Input.GetAxis("Horizontal");
    }
}
