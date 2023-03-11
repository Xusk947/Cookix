using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Burger Item", menuName = "Cookix/Items/Burger Item")]
public class BurgerItem : FoodItem
{
    public GameObject TopBunPrefab, BottomBunPrefab;

    public override FoodEntity Merge(FoodEntity baseFoodItem, FoodEntity combineFoodItem, CombineData combineData)
    {
        FoodEntity newFoodEntity = new GameObject().AddComponent<FoodEntity>();
        newFoodEntity.item = baseFoodItem.item;
        newFoodEntity.name = "MergerBurger";
        newFoodEntity.transform.position = Vector3.zero;
        // Transfer all copied Items to new Food Entity, then add new combined Item 
        newFoodEntity.addedItems = new List<CombineData>(baseFoodItem.addedItems);
        // Set Bottom Bun
        GameObject bottomBun = Instantiate(BottomBunPrefab);
        bottomBun.transform.SetParent(newFoodEntity.transform);
        // Added added items to new Entity
        float lastHeight = bottomBun.transform.GetChild(0).GetComponent<MeshRenderer>().bounds.max.y;
        foreach(CombineData combineDataInBase in newFoodEntity.addedItems)
        {
            GameObject ingredient = Instantiate(combineDataInBase.Output.Prefab);
            ingredient.transform.SetParent(newFoodEntity.transform);
            ingredient.transform.position += new Vector3(0, lastHeight, 0);
            // Check if MeshComponent exist in main ingredient else find this component in Children
            MeshRenderer meshRenderer;
            if (!NodeHasComponent(ingredient.gameObject, out meshRenderer))
            {
                meshRenderer = ingredient.GetComponentInChildren<MeshRenderer>();
            }
            lastHeight = meshRenderer.bounds.max.y;
        }
        // Finish method with adding Top Bun for new Food Entity
        GameObject topBun = Instantiate(TopBunPrefab);
        topBun.transform.SetParent(newFoodEntity.transform);
        topBun.transform.position += new Vector3(0, lastHeight, 0);
        // Destroy all Items
        Destroy(baseFoodItem.gameObject);
        Destroy(combineFoodItem.gameObject);
        // Create Grid and Generate Icon for Bun 
        GameObject grid = Instantiate<GameObject>(Content.Instance.FoodItemGridUI);
        GameObject bunIcon = GenerateIcon();
        bunIcon.transform.SetParent(grid.transform);
        // Generate Icons for Added Items and insert them into grid
        for(int i = 0; i < newFoodEntity.addedItems.Count; i++)
        {
            FoodItem foodItem = newFoodEntity.addedItems[i].Input;
            GameObject foodItemIcon = foodItem.GenerateIcon();
            foodItemIcon.transform.SetParent(grid.transform);
        }
        // Add a Grid Child
        grid.transform.SetParent(newFoodEntity.transform);
        return newFoodEntity;
    }

    private static bool NodeHasComponent<T>(GameObject obj, out T component) where T : Component
    {
        component = obj.GetComponent<T>();
        return component != null;
    }
}
