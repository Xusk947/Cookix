using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kitchen Item", menuName = "Cookix/Items/Kitchen Item")]
public class KitchenItem : Item
{
    public int maxHoldItems;
    public CookingType useFor;
    public new KitchenItemEntity Create()
    {
        KitchenItemEntity kitchenItemEntity = Instantiate(Prefab).AddComponent<KitchenItemEntity>();
        kitchenItemEntity.item = this;

        return kitchenItemEntity;
    }
}

public enum CookingType
{
    Frying, Mixing, Boiling
}