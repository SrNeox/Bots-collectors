using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerResources : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private PoolResource _poolResource;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds delay = new(5);

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            GameObject recourse = _poolResource.TakeObject();

            recourse.transform.position = _spawnPoints[i].position;

            yield return delay;
        }
    }
}
