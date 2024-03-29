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
        Events.KitchenBlockSecondInteract += OnKitchenBlockSecondInteract;
    }

    private void OnKitchenBlockUnselected(object sender, BlockArgs e)
    {
        e.Block.BlockHover(false);
    }

    private void OnKitchenBlockSelected(object sender, BlockArgs e)
    {
        e.Block.BlockHover(true);
    }

    private void OnKitchenBlockInteract(object sender, BlockArgs e)
    {
        e.Block.Interact(e.ChefController);
    }

    private void OnKitchenBlockSecondInteract(object sender, BlockArgs e)
    {
        e.Block.SecondInteract(e.ChefController, e.PressCondition);
    }

    private void OnDestroy()
    {
        Events.KitchenBlockInteract -= OnKitchenBlockInteract;
        Events.KitchenBlockSelected -= OnKitchenBlockSelected;
        Events.KitchenBlockUnselected -= OnKitchenBlockUnselected;
        Events.KitchenBlockSecondInteract -= OnKitchenBlockSecondInteract;
    }
}
