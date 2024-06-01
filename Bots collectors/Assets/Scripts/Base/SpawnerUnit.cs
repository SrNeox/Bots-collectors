using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerUnit : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private PoolUnit _poolUnit;
    [SerializeField] private Base _base;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    public void SpawnNewUnit()
    {
        Unit unit = _poolUnit.GetUnit();

        unit.transform.position = _spawnPoint[Random.Range(0, _spawnPoint.Length)].position;

        _base.AddUnit(unit);
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < _spawnPoint.Length; i++)
        {
            Unit unit = _poolUnit.GetUnit();

            unit.transform.position = _spawnPoint[i].position;

            _base.AddUnit(unit);

            yield return null;
        }
    }
}
