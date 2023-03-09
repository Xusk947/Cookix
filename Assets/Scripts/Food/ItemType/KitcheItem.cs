using UnityEngine;

public class KitchenItem : Item
{
    public int maxHoldItems;
    public CookingType useFor;
    public enum CookingType
    {
        Frying, Mixing, Boiling
    }
}
