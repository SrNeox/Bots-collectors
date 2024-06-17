using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private SpawnerUnit _spawnerUnit;
    [SerializeField] private ResourceFinder _resourceFinder;
    [SerializeField] private ScoreItem _scoreItem;
    [SerializeField] private SpawnerNewBase _spawnerNewBase;
    [SerializeField] private Color—hanger _color—hanger;
    [SerializeField] private ResourceStorage _resourceStorage;

    public void Initialize(PoolUnit poolUnit, TextMeshProUGUI text, Base prefab, PoolResource poolResource)
    {
        _spawnerUnit.SetPoolUnit(poolUnit);
        _scoreItem.SetText(text);
        _spawnerNewBase.SetPrefabBase(prefab);
        _resourceStorage.SetPoolResource(poolResource);
    }

    public void GiveInfo(out PoolUnit poolUnit, out TextMeshProUGUI text, out Base prefab, out PoolResource poolResource)
    {
        poolUnit = _spawnerUnit.GiveInfoPool();
        text = _scoreItem.GiveInfoText();
        prefab = _spawnerNewBase.GiveInfoPreafab();
        poolResource = _resourceStorage.GiveInfoPool();
    }

    public bool CanPlaceFlag { get; private set; }

    public event Action<int> AmountResourceUpdated;

    private void OnEnable()
    {
        _resourceFinder.FoundItem += AddResource;
        _resourceStorage.AddedResource += AssignWork;
        _spawnerNewBase.SetFlag += FlagPlaced;
        _spawnerUnit.SpentResource += GiveResource;
    }

    private void OnDisable()
    {
        _resourceFinder.FoundItem -= AddResource;
        _resourceStorage.AddedResource -= AssignWork;
        _spawnerNewBase.SetFlag -= FlagPlaced;
        _spawnerUnit.SpentResource -= GiveResource;
    }

    private void Start()
    {
        CanPlaceFlag = false;
    }

    private void OnMouseUp()
    {
        CanPlaceFlag = true;
        _color—hanger.ChangeColor();
    }

    public void FlagPlaced()
    {
        CanPlaceFlag = false;
        _color—hanger.RestoreColor();
    }

    private void GiveResource(int amountResource)
    {
        _resourceStorage.SpendResource(amountResource);
        AmountResourceUpdated?.Invoke(_resourceStorage.CountResource);
    }

    public void TakeItem(Item item)
    {
        _resourceStorage.IncreaseCount();
        _resourceStorage.RemoveIdenticalResource(item);
        _resourceStorage.ReturnPool(item);
        AmountResourceUpdated?.Invoke(_resourceStorage.CountResource);
    }

    public void AssignWork()
    {
        Unit unit = _spawnerUnit.GiveFreeUnit();

        if (unit != null && _resourceStorage._resources.Count > 0)
        {
            Item item = _resourceStorage.GiveInfoResource();
            unit.GoToPoint(item.transform);
            unit.SetItem(item);
        }
    }

    public Unit GiveFreeUnit()
    {
        return _spawnerUnit.GiveFreeUnit();
    }

    public int GiveInfoCountResorce()
    {
        return _resourceStorage.CountResource;
    }

    private void AddResource(Item item)
    {
        _resourceStorage.CheckResource(item);
    }
}