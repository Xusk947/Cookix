using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content : MonoBehaviour
{
    // GUI for FoodItems
    public static Content Instance;
    [HideInInspector]
    public GameObject FoodItemUI;
    [HideInInspector]
    public GameObject FoodItemGridUI;
    [HideInInspector]
    public GameObject Icon;
    [HideInInspector]
    public GameObject CookProgressBar;
    [HideInInspector]
    public GameObject ScreenshotBackground;
    private void Awake()
    {
        FoodItemUI = Resources.Load<GameObject>("Models/Prefabs/UI/FoodItemUI");
        FoodItemGridUI = Resources.Load<GameObject>("Models/Prefabs/UI/FoodItemGridUI");
        Icon = Resources.Load<GameObject>("Models/Prefabs/UI/IconImage");
        CookProgressBar = Resources.Load<GameObject>("Models/Prefabs/UI/CookProgressBar");
        ScreenshotBackground = Resources.Load<GameObject>("Models/Prefabs/UI/Background");

        Instance = this;
    }
}
