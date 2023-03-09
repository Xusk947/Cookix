using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEventsHandler : MonoBehaviour
{
    public void Start()
    {
        Events.KitchenBlockInteract += OnKitchenBlockInteract;
        Events.KitchenBlockSelected += OnKitchenBlockSelected;
        Events.KitchenBlockUnselected += OnKitchenBlockUnselected;
    }

    private void OnKitchenBlockUnselected(object sender, BlockArgs e)
    {
        e.Block.blockRenderer.material.color = e.Block.baseColor;
    }

    private void OnKitchenBlockSelected(object sender, BlockArgs e)
    {
        e.Block.blockRenderer.material.color = e.Block.smoothColor;
    }

    private void OnKitchenBlockInteract(object sender, BlockArgs e)
    {
        e.Block.Interact(e.PlayerController);
    }
}
