using System.Collections.Generic;
using UnityEngine;

public class PoolObject<T> where T : MonoBehaviour
{
    private Queue<T> _poolObject = new();
    private T _prefab;

    public PoolObject(T prefab)
    {
        _prefab = prefab;
    }

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
        item.gameObject.SetActive(false);
        _poolObject.Enqueue(item);
    }

    private void CreateObject()
    {
        var item = Object.Instantiate(_prefab);
        item.gameObject.SetActive(false);

        _poolObject.Enqueue(item);
    }
}
