using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class Content
{
    public static Content Instance { get; private set; }
    // Materials
    public Material Burnt;
    // GUI for FoodItems
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
    // Icons 
    public IconHide IconCancel, IconAccept;
    public Animator WarningIcon;
    // Sprites
    [HideInInspector]
    public Sprite Pixel1x1;
    // Person Prefab
    public ClientController Person;
    public void Load()
    {
        Burnt = Resources.Load<Material>("Models/Items/Food/Material/Burnt");

        FoodItemUI = Resources.Load<GameObject>("Models/Prefabs/UI/FoodItemUI");
        FoodItemGridUI = Resources.Load<GameObject>("Models/Prefabs/UI/FoodItemGridUI");
        FoodTaskUI = Resources.Load<FoodTaskImage>("Models/Prefabs/UI/FoodTaskUI");
        Icon = Resources.Load<GameObject>("Models/Prefabs/UI/IconImage");

        CookProgressBar = Resources.Load<GameObject>("Models/Prefabs/UI/CookProgressBar");

        Pixel1x1 = Resources.Load<Sprite>("Sprites/UI/pixel1x1");

        IconCancel = Resources.Load<IconHide>("Models/Prefabs/UI/CancelIcon");
        IconAccept = Resources.Load<IconHide>("Models/Prefabs/UI/AcceptIcon");
        WarningIcon = Resources.Load<Animator>("Models/Prefabs/UI/WarningIcon");

        Person = Resources.Load<ClientController>("Models/Prefabs/Persons/person-walker");

        Instance = this;
    }
}
