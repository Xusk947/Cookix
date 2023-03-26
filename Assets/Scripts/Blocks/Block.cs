using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [HideInInspector] private Renderer _blockRenderer;
    [HideInInspector] private List<Color> _baseColor;
    [HideInInspector] private List<Color> _smoothColor;
    protected void Start()
    {
        if (transform.childCount > 0)
        {
            _blockRenderer = transform.GetComponentInChildren<Renderer>();
        } else
        {
            _blockRenderer = transform.GetComponent<Renderer>();
        }

        _baseColor = new List<Color>();
        foreach (Material material in _blockRenderer.materials)
        {
            _baseColor.Add(material.color);
        }

        _smoothColor = new List<Color>();
        foreach (Material material in _blockRenderer.materials)
        {
            _smoothColor.Add(material.color * 1.4f);
        }
    }

    public virtual void Interact(PlayerController player)
    {

    }

    public virtual void SecondInteract(PlayerController player, bool isPress)
    {

    }

    public void BlockSelection(bool focused)
    {
        List<Color> colorToSet;
        if (focused)
        {
            colorToSet = _smoothColor;
        } else
        {
            colorToSet = _baseColor;
        }
        for (int i = 0; i < _blockRenderer.materials.Length; i++)
        {
            _blockRenderer.materials[i].color = colorToSet[i];
        }
    }
}
