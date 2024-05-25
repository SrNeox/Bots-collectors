using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerUnit : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private PoolUnit _poolUnit;
    [SerializeField] private Base _base;

    private void Awake()
    {
        StartCoroutine(Spawn());
    }

    public void SpawnNewUnit()
    {
        GameObject unit = _poolUnit.TakeObject();

        unit.transform.position = _spawnPoint[Random.Range(0, _spawnPoint.Length)].position;

        AddListBase(unit);
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < _spawnPoint.Length; i++)
        {
            GameObject unit = _poolUnit.TakeObject();

            unit.transform.position = _spawnPoint[i].position;

            AddListBase(unit);

            yield return null;
        }
    }

    private void AddListBase(GameObject unit)
    {
        _base.AddUnit(unit);
    }
}
