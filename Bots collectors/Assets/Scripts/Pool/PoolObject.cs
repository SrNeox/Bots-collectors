using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField] private int _ountPreload;

    protected GameObject Prefab;

    private Queue<GameObject> _poolItem = new();

    public GameObject TakeObject()
    {
        if (_poolItem.Count == 0)
        {
            _poolItem.Enqueue(CreateObject());
        }

        GameObject item = _poolItem.Dequeue();

        item.SetActive(true);

        return item;
    }

    public void ReturnObject(GameObject gameObject)
    {
        gameObject.transform.SetParent(transform, false);
        gameObject.SetActive(false);
        _poolItem.Enqueue(gameObject);
    }

    private GameObject CreateObject()
    {
        GameObject item = Instantiate(Prefab);

        item.SetActive(false);

        return item;
    }
}