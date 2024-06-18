using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField, Min(1f)] private float _speed;

    private Coroutine _goInDirection;
    private float _minDistance = 0.3f;

    public void StartGoInDirection(Transform point)
    {
        if(_goInDirection != null)
        {
            StopCoroutine(_goInDirection);
            _goInDirection = null;
        }

        _goInDirection = StartCoroutine(GoInDirection(point));
    }

    public void StopGoInDirection()
    {
        if (_goInDirection != null)
        {
            StopCoroutine(_goInDirection);
            _goInDirection = null;
        }
    }

    private IEnumerator GoInDirection(Transform point)
    {
        while (Vector3.Distance(transform.position, point.position) > _minDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, point.position, _speed * Time.deltaTime);

            yield return null;
        }
    }
}
