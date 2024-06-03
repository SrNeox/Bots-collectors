using System;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private PoolResource _poolResource;
    [SerializeField] private ResourceFinder _resourceFinder;
    [SerializeField] private ScoreItem _scoreItem;

    private List<Unit> _units = new();
    private Queue<Item> _resources = new();
    private List<Item> _foundItems = new();

    public int CountResource { get; private set; }

    private void OnEnable()
    {
        _resourceFinder.FoundItem += LearnAboutResource;
    }

    private void OnDisable()
    {
        _resourceFinder.FoundItem -= LearnAboutResource;
    }

    public void AddUnit(Unit unit)
    {
        unit.SetBase(this);
        _units.Add(unit);
    }

    public void TakeItem(Item item)
    {
        ++CountResource;
        RemoveIdenticalResource(item);
        _poolResource.ReturnItem(item);
        _scoreItem.UpdateScore();
    }

    public void AssignWork()
    {
        for (int i = 0; i < _units.Count; i++)
        {
            if (_units[i].IsAtWork == false && _resources.Count > 0)
            {
                Item item = _resources.Dequeue();
                _units[i].Activate();
                _units[i].GoToResource(item);

                return;
            }
        }
    }

    private void LearnAboutResource(Item item)
    {
        if(!_foundItems.Contains(item)) 
        {
            AddResource(item);
        }
    }

    private void AddResource(Item item)
    {
        _foundItems.Add(item);
        _resources.Enqueue(item);
        AssignWork();
    }

    private void RemoveIdenticalResource(Item item)
    {
        _foundItems.Remove(item);
    }
}