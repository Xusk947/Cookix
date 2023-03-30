using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container of a base player interactions
/// </summary>
public abstract class Block : MonoBehaviour
{
    /// <summary>
    /// A renderer component of block, used to add an effects with colors
    /// </summary>
    [HideInInspector] private Renderer _blockRenderer;
    /// <summary>
    /// is a default color in inspector
    /// </summary>
    [HideInInspector] private List<Color> _baseColor;
    /// <summary>
    /// color when block is hovered by player
    /// </summary>
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
    /// <summary>
    /// Use
    /// </summary>
    /// <param name="player"> is Player who interact with this block</param>
    public virtual void Interact(PlayerController player)
    {

    }
    /// <summary>
    /// Second Interact is the option for controll when Player keep hold a button
    /// </summary>
    /// <param name="player">who interact with this block</param>
    /// <param name="isPress">Use in Update methods to know when button is press off</param>
    public virtual void SecondInteract(PlayerController player, bool isPress)
    {

    }
    /// <summary>
    /// When player eyes interact with it
    /// </summary>
    /// <param name="focused">is in player eyes</param>
    public virtual void BlockHover(bool focused)
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
