using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemTask", menuName = "Cookix/Item Task/ItemTask")]
public class FoodTask : ScriptableObject
{
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    protected FoodItem _foodItem;
    
    public Sprite Icon { get { return _icon; } }
    public FoodItem FoodItem { get { return _foodItem; } }

    public void Awake()
    {
        if (_icon == null)
        {
            FoodEntity forScreenShot = GetFoodEntity();

            _icon = ScreenshotManager.Instance.TakeFoodEntityScreenShot(GetFoodEntity(), true);
        }
    }

    public virtual bool Compare(FoodEntity item)
    {
        return item.foodItem == _foodItem;
    }

    public virtual List<FoodItem> GetIngredients()
    {
        return new List<FoodItem>() { _foodItem };
    }

    protected FoodEntity GetFoodEntity()
    {
        FoodEntity entity = _foodItem.Create();

        return entity;
    }
}
