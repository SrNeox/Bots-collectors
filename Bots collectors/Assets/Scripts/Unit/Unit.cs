using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Base _base;
    private Item _item;
    private UnitMover _mover;
    private Flag _flag;
    private float _position—orrection = 2;

    public event Action UnitReadyToBuild;
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

        if (collider.TryGetComponent(out Flag flag))
        {
            if (_flag == flag)
            {
                UnitReadyToBuild?.Invoke();
            }
        }
    }

    public void GoToPoint(Transform point)
    {
        Activate();
        _mover.StartGoInDirection(point);
    }

    public void SetBase(Base newbase) => _base = newbase;

    public void SetItem(Item item) => _item = item;

    public void SetFlag(Flag flag) => _flag = flag;

    public void Activate() => IsAtWork = true;

    private void Deactivate() => IsAtWork = false;

    private void TakeItem(Collider collider)
    {
        collider.transform.SetParent(transform, false);
        collider.transform.position = new Vector3(transform.position.x, transform.position.y + _position—orrection, transform.position.z);
    }
}