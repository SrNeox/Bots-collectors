using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color—hanger : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _material;
    [SerializeField] private Color _highlight—olor;

    public void ChangeColor()
    {
        _renderer.material.color = _highlight—olor;
    }

    public void RestoreColor()
    {
        _renderer.material.color = _material.color;
    }
}
