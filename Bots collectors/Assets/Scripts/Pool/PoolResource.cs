using UnityEngine;

public class PoolResource : MonoBehaviour
{
    [SerializeField] Item _prefabItem;
    [SerializeField] private int _startCount;

    private PoolObject<Item> _poolItem;

    private void Awake()
    {
        _poolItem = new(_prefabItem, _startCount);
    }

    public Item GetItem()
    {
        Item item = _poolItem.GetObject();

        return item;
    }

    public void ReturnItem(Item item)
    {
        item.gameObject.transform.SetParent(transform, false);
        _poolItem.ReturnObject(item);
    }
}
