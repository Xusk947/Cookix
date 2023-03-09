using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content : MonoBehaviour
{
    public static Content Instance;
    public GameObject FoodItemUI;
    public GameObject FoodItemGridUI;
    public GameObject Icon;

    private void Awake()
    {
        FoodItemUI = Resources.Load<GameObject>("Models/Prefabs/UI/FoodItemUI");
        FoodItemGridUI = Resources.Load<GameObject>("Models/Prefabs/UI/FoodItemGridUI");
        Icon = Resources.Load<GameObject>("Models/Prefabs/UI/IconImage");
        Instance = this;
    }
    private void Start()
    {
    }
}
