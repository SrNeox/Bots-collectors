using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceFinder : MonoBehaviour
{
    [SerializeField] private Base _base;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Item item))
        {
            _base.LearnAboutResource(item);
            _base.PoisonWork();
        }
    }
}
