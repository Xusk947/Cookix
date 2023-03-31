using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food Item", menuName = "Cookix/Items/Food Item")]
public class FoodItem : Item
{
    // Placing on Plate
    public bool canBePlacedOnPlate;
    [ConditionalField("canBePlacedOnPlate", true)]
    public FoodItem platePrefab;
    // Sliced
    [Space(10)]
    public bool canBeSliced;
    [ConditionalField("canBeSliced", true)]
    public FoodItem slicedPrefab;
    // Fried
    [Space(10)]
    public bool canBeFried;
    [ConditionalField("canBeFried", true)]
    public FoodItem friedPrefab;
    // Boiling
    [Space(10)]
    public bool canBeBoiled;
    [ConditionalField("canBeBoiled", true)]
    public FoodItem boiledPrefab;
    // Mixing
    [Space(10)]
    public bool canBeMixed;
    [ConditionalField("canBeMixed", true)]
    public FoodItem mixedPrefab;
    // Combining
    [Space(10), SerializeField]
    private List<CombineData> combineWith = new List<CombineData>();

    public new FoodEntity Create(bool showIcon = true)
    {
        FoodEntity foodEntity = Instantiate(Prefab).AddComponent<FoodEntity>();
        foodEntity.item = this;

        if (Icon != null && showIcon)
        {
            GameObject icon = GenerateIcon(ScreenshotManager.Instance.TakeFoodEntityScreenShot(foodEntity));
            icon.transform.SetParent(foodEntity.transform);
            float scaleFactor = 1 / foodEntity.transform.localScale.x;
            icon.transform.localPosition = new Vector3(0, 0.5f, 0) * scaleFactor;
            icon.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        }

        return foodEntity;
    }

    public virtual FoodEntity Merge(FoodEntity baseFoodItem, FoodEntity combineFoodItem, CombineData combineData)
    {
        Destroy(baseFoodItem.gameObject);
        Destroy(combineFoodItem.gameObject);
        FoodEntity newFoodEntity = combineData.Output.Create();

        return newFoodEntity;
    }
    public void AddItem(FoodItem input, FoodItem output)
    {
        combineWith.Add(new CombineData(input, output));
    }

    public void RemoveItem(CombineData item)
    {
        combineWith.Remove(item);
    }

    public bool ContainsItem(CombineData item)
    {
        return combineWith.Contains(item);
    }

    public List<CombineData> CombineWith { get { return combineWith; } }
}

[Serializable]
public class CombineData
{
    public FoodItem Input; // Item on Input
    public FoodItem Output; // Item on Output

    public CombineData(FoodItem input, FoodItem output) 
    {
        Input = input;
        Output = output; 
    }
}