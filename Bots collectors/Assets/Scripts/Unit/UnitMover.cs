using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField, Min(1f)] private float _speed;

    private Coroutine _goToResource;
    private Coroutine _goToBase;

    public void StartGoResource(Transform resourse)
    {
        if (_goToResource != null) 
            StopGoResource();

        _goToResource = StartCoroutine(GoToResource(resourse));
    }

    public void StartGoBase(Transform headquarters)
    {
        if (_goToBase != null) 
            StopGoBase();

        _goToBase = StartCoroutine(GoToBase(headquarters));
    }

    public void StopGoBase()
    {
        if (_goToBase != null)
        {
            StopCoroutine(_goToBase);

            _goToBase = null;
        }
    }

    public void StopGoResource()
    {
        if (_goToResource != null)
        {
            StopCoroutine(_goToResource);

            _goToResource = null;
        }
    }

    private IEnumerator GoToResource(Transform resourse)
    {
        while (Vector3.Distance(transform.position, resourse.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, resourse.position, _speed * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator GoToBase(Transform headquarters)
    {
        while (Vector3.Distance(transform.position ,headquarters.transform.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, headquarters.position, _speed * Time.deltaTime);
            yield return null;
        }
    }
}
