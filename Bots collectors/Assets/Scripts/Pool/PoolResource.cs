using UnityEngine;

public class PoolResource : PoolObject
{
    [SerializeField] Item _prefabItem;

    private void Awake()
    {
        Prefab = _prefabItem.gameObject;
    }
}
