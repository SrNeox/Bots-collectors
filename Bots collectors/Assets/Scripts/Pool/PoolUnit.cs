using UnityEngine;

public class PoolUnit : MonoBehaviour
{
    [SerializeField] private Unit _prefabUnit;

    private PoolObject<Unit> _poolUnit;

    private void Awake()
    {
        _poolUnit = new(_prefabUnit);
    }

    public Unit GetUnit()
    {
        Unit unit = _poolUnit.GetObject();

        return unit;
    }
    
    public void ReturnUnit(Unit unit)
    {
        _poolUnit.ReturnObject(unit);
    }
}
