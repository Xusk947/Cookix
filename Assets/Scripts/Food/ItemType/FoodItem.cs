﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Food Item", menuName = "Cookix/Items/Food Item")]
public class FoodItem : Item
{
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
    // Can Be boiled
    [Space(10)]
    public bool canBeBoiled;
    // Combining
    [Space(10), SerializeField]
    private List<CombineData> combineWith = new List<CombineData>();

    public new FoodEntity Create()
    {
        FoodEntity foodEntity = Instantiate(Prefab).AddComponent<FoodEntity>();
        foodEntity.item = this;

        if (Icon != null)
        {
            GameObject icon = GenerateIcon();
            icon.transform.SetParent(foodEntity.transform);
            icon.transform.localPosition = new Vector3(0, 0.5f, 0);
            UnityEngine.Debug.Log(icon);
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