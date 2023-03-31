using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    /// <summary>
    /// When player hover a block
    /// </summary>
    public static event EventHandler<BlockArgs> KitchenBlockSelected;
    /// <summary>
    /// When player last hovered block lost focus
    /// </summary>
    public static event EventHandler<BlockArgs> KitchenBlockUnselected;
    /// <summary>
    /// When player press Interaction Key
    /// </summary>
    public static event EventHandler<BlockArgs> KitchenBlockInteract;
    /// <summary>
    /// When player press or release Second Interaction Key
    /// </summary>
    public static event EventHandler<BlockArgs> KitchenBlockSecondInteract;

    public static void OnKitchenBlockSelected(BlockArgs args)
    {
        KitchenBlockSelected?.Invoke(null, args);
    }

    public static void OnKitchenBlockUnselected(BlockArgs args)
    {
        KitchenBlockUnselected?.Invoke(null, args);
    }

    public static void OnKitchenBlockInteract(BlockArgs args)
    {
        KitchenBlockInteract?.Invoke(null, args);
    }

    public static void OnKitchenBlockSecondInteract(BlockArgs args)
    {
        KitchenBlockSecondInteract?.Invoke(null, args);
    }
}
