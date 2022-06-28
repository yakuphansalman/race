using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Mechanics : MonoBehaviour
{
    [SerializeField] private Car_Preferences_SO _carPrefs = null;

    private bool _canAcc = true;

    private int _gear;

    private float _gearForce;

    public int gear => _gear;

    public float gearForce => _gearForce;


    #region Singleton
    private static Car_Mechanics instance = null;
    public static Car_Mechanics Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("SingletonCM").AddComponent<Car_Mechanics>();
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
        ManageGears();
    }
    private void ManageGears()
    {
        for (int i = 0; i < _carPrefs.a_gearLimits.Length -1; i++)
        {
            if (Car_Physics.Instance.speed > _carPrefs.a_gearLimits[i] && Car_Physics.Instance.speed <= _carPrefs.a_gearLimits[i + 1] && _canAcc)
            {
                _gearForce = Mathf.Pow(_carPrefs.m_gear, i+1);
                StartCoroutine(GearUpAndDown(i + 1));
            }
        }
    }
    private IEnumerator GearUpAndDown(int gear)
    {
        _canAcc = false;
        _gear = gear;
        yield return new WaitForSeconds(0.5f);
        _canAcc = true;
    }
}
