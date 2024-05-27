using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Base _base;
    private Item _item;
    private Coroutine _goToResourse;
    private Coroutine _goToBase;

    public bool IsAtWork { get; private set; }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Item item) && _item == null)
        {
            _item = item;
            StopCoroutine(_goToResourse);
            TakeItem(collider);
            StartGoBase();
        }

        if (collider.TryGetComponent(out Base _))
        {
            IsAtWork = false;
            StopCoroutine(_goToBase);
            _base.TakeItem(_item);
            _item = null;
        }
    }


    public void SetBase(Base basePosition)
    {
        _base = basePosition;
    }

    public void StartGoResourse(Transform resourse)
    {
        IsAtWork = true;
        _goToResourse = StartCoroutine(GoToResourse(resourse));
    }

    private void StartGoBase()
    {
        _goToBase = StartCoroutine(GoToBase());
    }

    private IEnumerator GoToResourse(Transform resourse)
    {
        while (transform.position != resourse.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, resourse.position, _speed * Time.deltaTime);
            IsAtWork = true;

            yield return null;
        }
    }

    private IEnumerator GoToBase()
    {
        while (transform.position != _base.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _base.transform.position, _speed * Time.deltaTime);
            yield return null;
        }
    }

    private void TakeItem(Collider collider)
    {
        collider.transform.SetParent(transform, false);
        collider.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
    }
}
