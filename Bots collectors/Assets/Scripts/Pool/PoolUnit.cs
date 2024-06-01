using UnityEngine;

public class PoolUnit : MonoBehaviour
{
    [SerializeField] private Unit _prefabUnit;
    [SerializeField] private int _startCount;

    private PoolObject<Unit> _poolUnit;

    private void Awake()
    {
        _poolUnit = new(_prefabUnit, _startCount);
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
