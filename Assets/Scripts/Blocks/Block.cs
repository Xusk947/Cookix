using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [HideInInspector] public Renderer blockRenderer;
    [HideInInspector] public Color baseColor;
    [HideInInspector] public Color smoothColor;
    private void Start()
    {
        if (transform.childCount > 0)
        {
            blockRenderer = transform.GetComponentInChildren<Renderer>();
        } else
        {
            blockRenderer = transform.GetComponent<Renderer>();
        }
        baseColor = blockRenderer.material.color;
        smoothColor = blockRenderer.material.color * 0.8f;
    }

    public virtual void Interact(PlayerController player)
    {

    }
}
