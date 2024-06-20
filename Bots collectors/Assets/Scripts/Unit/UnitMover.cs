using System.Collections;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField, Range(1, 4)] private float _speed;

    private Coroutine _goInDirection;
    private float _minDistance = 0.3f;
    private float _minSqrDistance;

    private void Start()
    {
        _minSqrDistance = _minDistance * _minDistance;
    }

    public void StartGoInDirection(Transform point)
    {
        if (_goInDirection != null)
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
        while ((transform.position - point.position).sqrMagnitude > _minSqrDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, point.position, _speed * Time.deltaTime);

            yield return null;
        }
    }
}
