using System;
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

    public event Action<int> AmountResourceUpdated;

    public bool CanPlaceFlag { get; private set; }

    private void OnEnable()
    {
        if (_resourceFinder != null)
            _resourceFinder.FoundItem += AddResource;
        if (_resourceStorage != null)
            _resourceStorage.AddedResource += AssignWork;
        if (_spawnerNewBase != null)
            _spawnerNewBase.FlagPlaced += FlagPlaced;
        if (_spawnerUnit != null)
            _spawnerUnit.SpentResource += SpendResource;
    }

    private void OnDisable()
    {
        _resourceFinder.FoundItem -= AddResource;
        _resourceStorage.AddedResource -= AssignWork;
        _spawnerNewBase.FlagPlaced -= FlagPlaced;
        _spawnerUnit.SpentResource -= SpendResource;
    }

    private void OnMouseUp()
    {
        CanPlaceFlag = !CanPlaceFlag;
        _color—hanger.ChangeColor();
    }

    public void FlagPlaced()
    {
        CanPlaceFlag = false;
        _color—hanger.RestoreColor();
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

    public Unit GiveFreeUnit() => _spawnerUnit.GiveFreeUnit();

    public int GiveInfoCountResorce() => _resourceStorage.CountResource;

    public void Initialize(Unit unit, TextMeshProUGUI text, Base prefab, PoolResource poolResource)
    {
        _spawnerUnit.SetPoolUnit(unit);
        _scoreItem.SetText(text);
        _spawnerNewBase.SetPrefabBase(prefab);
        _resourceStorage.SetPoolResource(poolResource);
    }

    public void GiveInfo(out Unit unit , out TextMeshProUGUI text, out Base prefab, out PoolResource poolResource)
    {
        unit = _spawnerUnit.GiveInfoUnit();
        text = _scoreItem.GiveInfoText();
        prefab = _spawnerNewBase.GetInfoPreafab();
        poolResource = _resourceStorage.GiveInfoPool();
    }

    private void SpendResource(int amountResource)
    {
        _resourceStorage.SpendResource(amountResource);
        AmountResourceUpdated?.Invoke(_resourceStorage.CountResource);
    }

    private void AddResource(Item item) => _resourceStorage.CheckResource(item);
}