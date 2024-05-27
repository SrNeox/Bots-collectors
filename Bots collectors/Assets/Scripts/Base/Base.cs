using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private PoolResource _poolResource;

    private List<Unit> _units = new();
    private List<Item> _resources = new();

    public int CountResourse { get; private set; }

    public void AddUnit(Unit unit)
    {
        unit.SetBase(this);
        _units.Add(unit);
    }

    public void TakeItem(Item item)
    {
        item.SetBusy(false);

        ++CountResourse;

        _poolResource.ReturnObject(item.gameObject);
    }

    public void PoisonWork()
    {
        for (int i = 0; i < _units.Count; i++)
        {
            Unit unit = _units[i];

            if (unit.IsAtWork == false)
            {
                foreach (Item item in _resources)
                {   
                    if (item.IsBusy != true)
                    {
                        item.SetBusy(true);
                        unit.StartGoResourse(item.transform);
                        _resources.Remove(item);

                        break;
                    }
                }
            }
        }
    }

    public void LearnAboutResource(Item item)
    {
        _resources.Add(item);
    }
}
