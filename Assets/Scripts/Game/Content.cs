using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content : MonoBehaviour
{
    // GUI for FoodItems
    public static Content Instance { get; private set; }
    // Food Zone
    [HideInInspector]
    public GameObject FoodItemUI;
    [HideInInspector]
    public GameObject FoodItemGridUI;
    [HideInInspector]
    public FoodTaskImage FoodTaskUI;
    [HideInInspector]
    public GameObject Icon;
    // Blocks UI
    [HideInInspector]
    public GameObject CookProgressBar;
    private void Awake()
    {
        FoodItemUI = Resources.Load<GameObject>("Models/Prefabs/UI/FoodItemUI");
        FoodItemGridUI = Resources.Load<GameObject>("Models/Prefabs/UI/FoodItemGridUI");
        FoodTaskUI = Resources.Load<FoodTaskImage>("Models/Prefabs/UI/FoodTaskUI");
        Icon = Resources.Load<GameObject>("Models/Prefabs/UI/IconImage");

        CookProgressBar = Resources.Load<GameObject>("Models/Prefabs/UI/CookProgressBar");

        Instance = this;
    }
}
