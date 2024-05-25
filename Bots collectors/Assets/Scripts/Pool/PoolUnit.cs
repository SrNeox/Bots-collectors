using UnityEngine;

public class PoolUnit : PoolObject
{
    [SerializeField] Unit _prefabUnit;

    private void Awake()
    {
        Prefab = _prefabUnit.gameObject;
    }
}
