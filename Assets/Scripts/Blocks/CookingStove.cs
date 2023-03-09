using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingStove : Table
{
    public enum ItemKind
    {
        None, Pan, Pot
    }

    public void Start()
    {
        
    }
    void Update()
    {
        if (itemEntity == null) return;
        if (itemEntity.item is not KitchenItem) return;
        KitchenItem kitchenItem = itemEntity.item as KitchenItem;
    }
}
