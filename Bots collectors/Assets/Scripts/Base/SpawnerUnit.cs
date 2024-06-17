using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerUnit : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private PoolUnit _poolUnit;
    [SerializeField] private Base _base;

    private readonly List<Unit> _units = new();

    private int _pricePerUnit = 3;

    public event Action<int> SpentResource;

    private void Start()
    {
        StartCoroutine(Spawn());
    }
    public void SetPoolUnit(PoolUnit poolUnit)
    {
        _poolUnit = poolUnit;
    }

    public PoolUnit GiveInfoPool()
    {
        return _poolUnit;
    }

    public Unit GiveFreeUnit()
    {
        foreach (var unit in _units)
        {
            if (unit.IsAtWork == false)
            {
                return unit;
            }
        }

        return null;
    }

    public void SpawnNewUnit()
    {
        if (_base.GiveInfoCountResorce() >= _pricePerUnit)
        {
            Unit unit = _poolUnit.GetUnit();
            unit.transform.position = _spawnPoint[UnityEngine.Random.Range(0, _spawnPoint.Length)].position;
            unit.SetBase(_base);
            _units.Add(unit);
            SpentResource?.Invoke(_pricePerUnit);
        }
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < _spawnPoint.Length; i++)
        {
            Unit unit = _poolUnit.GetUnit();
            unit.transform.position = _spawnPoint[i].position;
            unit.SetBase(_base);
            _units.Add(unit);

            yield return null;
        }
    }
}
