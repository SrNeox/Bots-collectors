using System;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private Unit _unit;

    public event Action CameConstruction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            if (unit == _unit)
            {
                CameConstruction.Invoke();
            }
        }
    }

    public void SetUnit(Unit unit) => _unit = unit;
}

