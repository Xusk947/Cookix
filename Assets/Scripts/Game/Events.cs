using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static event EventHandler<BlockArgs> KitchenBlockSelected;
    public static event EventHandler<BlockArgs> KitchenBlockUnselected;
    public static event EventHandler<BlockArgs> KitchenBlockInteract;
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
