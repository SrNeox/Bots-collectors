using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Base _base;
    private Item _item;
    private UnitMover _mover;

    public bool IsAtWork { get; private set; }

    private void Start()
    {
        _mover = GetComponent<UnitMover>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Item item))
        {
            if (_item == item)
            {
                TakeItem(collider);
                _mover.StartGoInDirection(_base.transform);
            }
        }

        if (collider.TryGetComponent(out Base baseComponent))
        {
            if (_base == baseComponent)
            {
                Deactivate();
                _mover.StopGoInDirection();
                _base.TakeItem(_item);
                _item = null;
            }
        }
    }

    public void GoToResource(Item item)
    {
        _item = item;
        Activate();
        _mover.StartGoInDirection(item.transform);
    }

    public void SetBase(Base newbase)
    {
        _base = newbase;
    }
     
    public void Activate()
    {
        IsAtWork = true;
    }

    private void Deactivate()
    {
        IsAtWork = false;
    }

    private void TakeItem(Collider collider)
    {
        collider.transform.SetParent(transform, false);
        collider.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
    }
}
