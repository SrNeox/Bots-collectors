using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerResources : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private PoolResource _poolResource;
    [SerializeField] private int _spawnDelay = 5;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds delay = new(_spawnDelay);

        while (true)
        {
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                Item recourse = _poolResource.GetItem();

                recourse.transform.position = _spawnPoints[i].position;

                yield return delay;
            }
        }
    }
}

