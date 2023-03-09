using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Block
{
    public override void Interact(PlayerController player)
    {
        if (player.CurrentItem == null) return;
        if (player.CurrentItem is not FoodEntity) return;
        player.RemoveItem();
    }
}
