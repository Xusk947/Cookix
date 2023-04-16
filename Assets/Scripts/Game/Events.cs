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
    /// <summary>
    /// When player finish ClientOrder
    /// </summary>
    public static event EventHandler<ClientArgs> ClientOrderFinish;
    /// <summary>
    /// When Client time is gone and order failed
    /// </summary>
    public static event EventHandler<ClientArgs> ClientOrderFail;

    public static event EventHandler UnderMinute;

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

    public static void OnClientOrderFinish(ClientArgs args)
    {
        ClientOrderFinish?.Invoke(null, args);
    }

    public static void OnClientOrderFail(ClientArgs args)
    {
        ClientOrderFail?.Invoke(null, args);
    }

    public static void OnUnderMinute()
    {
        UnderMinute?.Invoke(null, null);
    }
}
