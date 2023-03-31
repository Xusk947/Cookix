using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateEntity : ItemEntity
{
    private FoodEntity _itemOn;
    public FoodEntity ItemOn
    {
        get { return _itemOn; }
        set
        {
            _itemOn = value;
            if (value == null) return;
            PlaceItem(_itemOn);
        }
    }

    public bool TryToAddItemOn(FoodEntity foodEntity)
    {
        print(foodEntity + " : " + _itemOn);
        if (foodEntity == null) return false;

        if (_itemOn != null)
        {
            return TryToMerge(foodEntity);
        }

        if (!foodEntity.foodItem.canBePlacedOnPlate) return false;

        ItemOn = foodEntity;

        return true;
    }

    private bool TryToMerge(FoodEntity foodEntity)
    {
        FoodEntity merged;

        _itemOn.CanCombine(foodEntity, out merged);
        if (merged != null)
        {
            ItemOn = merged;
            return true;
        }
        return false;
    }
    private void PlaceItem(FoodEntity foodEntity)
    {
        _itemOn = foodEntity;
        if (_itemOn.foodItem.platePrefab != null)
        {
            // Some ingredients can also have a special prefab on plate, like a rice
            FoodEntity foodItem = Instantiate(_itemOn.foodItem.platePrefab.Prefab).AddComponent<FoodEntity>();
            foodItem.item = _itemOn.item;

            //Destroy(_itemOn);

            _itemOn = foodItem;
        }

        _itemOn.transform.SetParent(transform);
        _itemOn.transform.localPosition = new Vector3(0, 0, 0.01f);
    }
}
