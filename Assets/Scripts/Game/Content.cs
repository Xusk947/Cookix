using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content : MonoBehaviour
{
    // GUI for FoodItems
    public static Content Instance;
    public GameObject FoodItemUI;
    public GameObject FoodItemGridUI;
    public GameObject Icon;
    public GameObject CookProgressBar;
    private void Awake()
    {
        FoodItemUI = Resources.Load<GameObject>("Models/Prefabs/UI/FoodItemUI");
        FoodItemGridUI = Resources.Load<GameObject>("Models/Prefabs/UI/FoodItemGridUI");
        Icon = Resources.Load<GameObject>("Models/Prefabs/UI/IconImage");
        CookProgressBar = Resources.Load<GameObject>("Models/Prefabs/UI/CookProgressBar");

        Instance = this;
    }
}
