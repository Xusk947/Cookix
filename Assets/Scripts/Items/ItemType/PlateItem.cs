using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plate Item", menuName = "Cookix/Items/Plate Item")]
public class PlateItem : Item
{
    public new PlateEntity Create(bool showIcon = true)
    {
        PlateEntity plateEntity = Instantiate(Prefab).AddComponent<PlateEntity>();
        plateEntity.item = this;

        return plateEntity;
    }
}
