using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenItemEntity : ItemEntity
{
    public float cookingProgress = 0f;
    public bool isCooked;

    private List<FoodEntity> itemsInside = new List<FoodEntity>();
    
    public bool CanCook()
    {
        if (itemsInside.Count <= 0) return false;
        foreach (FoodEntity item in itemsInside)
        {
            if (!CanUse(item.foodItem)) return false;
        }
        return true;
    }
    public bool TryToAddItem(FoodEntity item)
    {
        Debug.Log(CanAddItem(item));
        if (CanAddItem(item))
        {
            AddItem(item);
            return true;
        }
        return false;
    }
    protected void AddItem(FoodEntity item)
    {
        item.transform.SetParent(transform);

        FoodEntity newFoodEntity = item.foodItem.Create();
        newFoodEntity.transform.SetParent(transform);
        newFoodEntity.transform.localPosition = new Vector3(0, 0, 0);
        newFoodEntity.transform.localScale = newFoodEntity.transform.localScale * 0.8f;
        itemsInside.Add(newFoodEntity);
        Destroy(item.gameObject);

        UpdateItemsPosition();

        Mathf.Clamp(cookingProgress, 0f, cookingProgress - 0.25f);
    }

    public void CookItemsInside()
    {
        FoodItem outputItem = GetPrefabFromItem(itemsInside[0].foodItem);

        RemoveItems();
        AddItem(outputItem.Create());
        isCooked = true;
    }

    public FoodEntity GetCookedItem()
    {
        if (itemsInside.Capacity > 0 && isCooked)
        {
            return itemsInside[0];
        }
        return null;
    }
    public void RemoveItems()
    {
        Debug.Log(itemsInside.Count);
        foreach (FoodEntity item in itemsInside) 
        {
            Destroy(item.gameObject);
        }
        itemsInside = new List<FoodEntity>();

        cookingProgress = 0f;
        isCooked = false;
    }

    private void UpdateItemsPosition()
    {
        float lastHeight = 0f;
        foreach (FoodEntity item in itemsInside)
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
        if (itemsInside.Count >= kitchenItem.maxHoldItems) return false;
        return CanUse(item.foodItem);
    }

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

    public KitchenItem kitchenItem { get { return item as KitchenItem; } }
}
