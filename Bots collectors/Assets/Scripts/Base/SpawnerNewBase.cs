using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnerNewBase : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private Base _prefab;
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private LayerMask _ground;

    private Flag _currentFlag;
    private Vector3 _pointSet;
    private int _priceNewBase = 5;
    private Unit _unit;

    public event Action SetFlag;

    private void FixedUpdate()
    {
        PutFlag();
    }

    public void SetPrefabBase(Base prefab)
    {
        _prefab = prefab;
    }

    public Base GiveInfoPreafab()
    {
        return _prefab;
    }

    public void SendBild()
    {
        _unit = _base.GiveFreeUnit();

        if (_base.GiveInfoCountResorce() >= _priceNewBase && _unit != null)
        {
            _unit.GoToPoint(_currentFlag.transform);
            _unit.SetFlag(_currentFlag);
        }
    }

    private void PutFlag()
    {
        if (_base.CanPlaceFlag == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _ground))
                {
                    Vector3 vector3 = new Vector3(hit.point.x, hit.point.y + 1, hit.point.z);

                    _pointSet = vector3;
                }

                ChekFlag();

                SetFlag.Invoke();
            }
        }
    }

    private void ChekFlag()
    {
        if (_currentFlag == null)
        {
            _currentFlag = Instantiate(_flagPrefab, _pointSet, Quaternion.identity);
            SendBild();
        }
        else
        {
            _currentFlag.transform.position = _pointSet;
            SendBild();
        }
    }
}