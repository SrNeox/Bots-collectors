using System;
using System.Linq;
using UnityEngine;

public class ResourceFinder : MonoBehaviour
{
    [SerializeField] private int _scanRadius;
    [SerializeField] private LayerMask _layerMask;

    public event Action<Item> FoundItem;

    public void Scan()
    {
        Collider[] resources = Physics.OverlapSphere(transform.position, _scanRadius, _layerMask);

        foreach (var resource in resources)
        {
            if (resource.TryGetComponent(out Item item))
            {
                FoundItem.Invoke(item);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawSphere(transform.position, _scanRadius);
    }
}
