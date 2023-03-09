using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenItemEntity : ItemEntity
{
    public float cookingProgress = 0f;

    private List<FoodItem> itemsInside = new List<FoodItem>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddItem(FoodEntity item)
    {
        item.transform.SetParent(transform);
        itemsInside.Add(item.foodItem);
        Destroy(item.gameObject);

        UpdateItemsPosition();

        Mathf.Clamp(cookingProgress, 0f, cookingProgress - 0.25f);
    }

    public void UpdateItemsPosition()
    {
        float lastHeight = 0f;
        foreach (FoodItem item in itemsInside)
        {
            GameObject newItem = Instantiate<GameObject>(item.Prefab);
            newItem.transform.SetParent(transform);
            newItem.transform.position += new Vector3(0, lastHeight, 0);
            // Check if MeshComponent exist in main ingredient else find this component in Children
            MeshRenderer meshRenderer = newItem.GetComponent<MeshRenderer>();
            if (meshRenderer == null)
            {
                meshRenderer = newItem.GetComponentInChildren<MeshRenderer>();
            }
            lastHeight = meshRenderer.bounds.max.y;
        }
    }

    public bool CanAddItem(FoodEntity item)
    {
        switch(kitchenItem.useFor)
        {
            case KitchenItem.CookingType.Boiling:
                return item.foodItem.canBeBoiled;
            case KitchenItem.CookingType.Frying:
                return item.foodItem.canBeFried;
            case KitchenItem.CookingType.Mixing:
                return false;
            default:
                return false;
        }
    }

    public KitchenItem kitchenItem { get { return item as KitchenItem; } }
}
