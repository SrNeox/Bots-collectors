using System.Collections.Generic;
using UnityEngine;

public class PoolObject<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;

    private Queue<T> _poolObject = new();

    public T GetObject()
    {
        if (_poolObject.Count == 0)
        {
            CreateObject();
        }

        var item = _poolObject.Dequeue();

        item.gameObject.SetActive(true);

        return item;
    }

    public void ReturnObject(T item)
    {
        if(item != null)
        {
            item.transform.SetParent(null, false);
            item.gameObject.SetActive(false);
            _poolObject.Enqueue(item);
        }
    }

    private void CreateObject()
    {
        var item = Instantiate(_prefab);
        item.gameObject.SetActive(false);

        _poolObject.Enqueue(item);
    }
}
