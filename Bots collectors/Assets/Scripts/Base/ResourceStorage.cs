using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    [SerializeField] private PoolResource _poolResource;

    private List<Item> _foundResources = new();

    public Queue<Item> _resources { get; private set; }

    public int CountResource { get; private set; }

    public event Action AddedResource;

    private void Start()
    {
        _resources = new();
    }

    public void SetPoolResource(PoolResource poolResource)
    {
        _poolResource = poolResource;
    }

    public PoolResource GiveInfoPool()
    {
        return _poolResource;   
    }

    public void RemoveIdenticalResource(Item item)
    {
        _foundResources.Remove(item);
    }

    public Item GiveInfoResource()
    {
        return _resources.Dequeue();
    }

    public void CheckResource(Item item)
    {
        if (!_foundResources.Contains(item))
        {
            AddResource(item);
        }
    }

    public void ReturnPool(Item item)
    {
        _poolResource.ReturnObject(item);
    }

    public void SpendResource(int countResource)
    {
        CountResource -= countResource;
    }

    public void IncreaseCount()
    {
        ++CountResource;
    }

    private void AddResource(Item item)
    {
        _foundResources.Add(item);
        _resources.Enqueue(item);
        AddedResource?.Invoke();
    }
}
