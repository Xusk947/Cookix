using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenItemEntity : ItemEntity
{
    /// <summary>
    /// Progress of cook, from 0 to 1
    /// </summary>
    public float CookingProgress = 0f;
    /// <summary>
    /// when food on it is done
    /// </summary>
    public bool IsCooked;
    /// <summary>
    /// List of items inside which was added to it
    /// </summary>
    private List<FoodEntity> _itemsInside = new List<FoodEntity>();
    
    /// <summary>
    /// Can uese this Item for cooking
    /// </summary>
    /// <returns>check count of items inside and Cook Type of each item</returns>
    public bool CanCook()
    {
        if (_itemsInside.Count <= 0) return false;
        foreach (FoodEntity item in _itemsInside)
        {
            if (!CanUse(item.foodItem)) return false;
        }
        return true;
    }
    /// <summary>
    /// Try to put item on themself so when it's added return true and automaticlly add Item as a new GameObject
    /// </summary>
    /// <param name="item">which item will be placed on it</param>
    /// <returns></returns>
    public bool TryToAddItem(FoodEntity item)
    {
        if (CanAddItem(item))
        {
            AddItem(item);
            return true;
        }
        return false;
    }

    public bool ItemIsBurning()
    {
        foreach(FoodEntity item in _itemsInside)
        {
            if (item.foodItem.itIsABurntPrefab) return true;
        }
        return false;
    }
    protected void AddItem(FoodEntity item)
    {
        item.transform.SetParent(transform);
        // Set transform to 0 and scale to 0.8
        FoodEntity newFoodEntity = item.foodItem.Create();
        newFoodEntity.transform.SetParent(transform);
        newFoodEntity.transform.localPosition = new Vector3(0, 0, 0);
        newFoodEntity.transform.localScale = newFoodEntity.transform.localScale * 0.8f;
        // Then add item from Holder to it self, and destroy this item in Holder
        _itemsInside.Add(newFoodEntity);
        Destroy(item.gameObject);

        UpdateItemsPosition();

        Mathf.Clamp(CookingProgress, 0f, CookingProgress - 0.25f);
    }

    public void CookItemsInside()
    {
        FoodItem outputItem = GetPrefabFromItem(_itemsInside[0].foodItem);

        RemoveItems();
        AddItem(outputItem.Create());
        IsCooked = true;
    }

    public FoodEntity GetCookedItem()
    {
        if (_itemsInside.Capacity > 0 && IsCooked)
        {
            return _itemsInside[0];
        }
        return null;
    }
    public void RemoveItems(bool destroyItems = true)
    {
        if (destroyItems)
        {
            foreach (FoodEntity item in _itemsInside) 
            {
                Destroy(item.gameObject);
            }
        }

        _itemsInside = new List<FoodEntity>();

        CookingProgress = 0f;
        IsCooked = false;
    }

    private void UpdateItemsPosition()
    {
        float lastHeight = 0f;
        foreach (FoodEntity item in _itemsInside)
        {
            // Place each item on top of it by Using Mesh Renderer bounds.size.y
            item.transform.localPosition = new Vector3(0, 0, 0.01f);
            item.transform.position += new Vector3(0, lastHeight, 0);
            MeshRenderer meshRenderer = item.GetComponent<MeshRenderer>();
            if (meshRenderer == null)
            {
                meshRenderer = item.GetComponentInChildren<MeshRenderer>();
            }
            lastHeight += meshRenderer.bounds.size.y;
        }
    }

    protected bool CanAddItem(FoodEntity item)
    {
        if (_itemsInside.Count >= kitchenItem.maxHoldItems) return false;
        return CanUse(item.foodItem);
    }
    /// <summary>
    /// Check for Cook Type of item and return true when it is the same
    /// </summary>
    /// <param name="item">which item will be checked for CookingType</param>
    /// <returns></returns>
    private bool CanUse(FoodItem item)
    {
        switch (kitchenItem.useFor)
        {
            case CookingType.Boiling:
                return item.canBeBoiled;
            case CookingType.Frying:
                return item.canBeFried;
            case CookingType.Mixing:
                return false;
            default:
                return false;
        }
    }
    /// <summary>
    /// Get Prefab of Item based cooking type
    /// </summary>
    /// <param name="item">return prefab of this cooked type</param>
    /// <returns></returns>
    private FoodItem GetPrefabFromItem(FoodItem item)
    {
        switch (kitchenItem.useFor)
        {
            case CookingType.Boiling:
                return null;
            case CookingType.Frying:
                return item.friedPrefab;
            case CookingType.Mixing:
                return null;
            default:
                return null;
        }
    }
    /// <summary>
    /// another way to get this Item already as KitchenItem
    /// </summary>
    public KitchenItem kitchenItem { get { return item as KitchenItem; } }
}
