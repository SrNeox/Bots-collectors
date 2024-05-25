using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private PoolResource _poolResource;

    private List<GameObject> _units = new();
    private List<Item> _resources = new();

    public int CountResourse { get; private set; }
     
    public void AddUnit(GameObject worker)
    {
        if (worker.TryGetComponent(out Unit unit) == true)
        {
            unit.SetBase(this);
            _units.Add(worker);
        }
    }

    public void TakeItem(Item item)
    {
        ++CountResourse;

        _poolResource.ReturnObject(item.gameObject);

        Debug.Log($"Ресурсов {CountResourse}");
    }

    public void PoisonWork()
    {
        for (int i = 0; i < _units.Count; i++)
        {
            GameObject worker = _units[i];

            worker.TryGetComponent(out Unit unit);

            if (unit.IsAtWork != true   )
            {
                Item item = _resources[0];

                unit.StartGoResourse(item.transform);

                _resources.RemoveAt(0);

                break;
            }
        }
    }

    public void LearnAboutResource(Item item)
    {
        _resources.Add(item);
    }
}
