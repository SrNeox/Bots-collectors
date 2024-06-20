using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerUnit : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private Base _base;
    [SerializeField] private Unit _prefabUnit;

    private readonly List<Unit> _units = new();

    private int _pricePerUnit = 3;

    public event Action<int> SpentResource;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    public void SetPoolUnit(Unit unit) => _prefabUnit = unit;

    public Unit GiveInfoUnit() => _prefabUnit;

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
            UnitSetup(_spawnPoint[UnityEngine.Random.Range(0, _spawnPoint.Length)].position);
            SpentResource?.Invoke(_pricePerUnit);
        }
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < _spawnPoint.Length; i++)
        {
            UnitSetup(_spawnPoint[i].position);

            yield return null;
        }
    }

    private void UnitSetup(Vector3 transform)
    {
        Unit unit = Instantiate(_prefabUnit, transform, Quaternion.identity);
        unit.SetBase(_base);
        _units.Add(unit);
    }
}
