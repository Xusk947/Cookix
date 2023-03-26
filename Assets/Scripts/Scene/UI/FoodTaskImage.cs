using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FoodTaskImage : MonoBehaviour
{
    private Image _bodyImage;
    private Image _foodHolder;
    private GridLayout _ingredientHolder;

    private RectTransform _bodyRect;

    private float _cellSize, _cellSpacingX;

    private FoodTask _task;

    public void Start()
    {
        _bodyImage = transform.GetChild(0).GetComponent<Image>();
        _bodyRect = transform.GetChild(0).GetComponent<RectTransform>();

        _foodHolder = _bodyImage.transform.GetChild(0).GetComponent<Image>();
        _foodHolder.preserveAspect = false;

        _ingredientHolder = _bodyImage.transform.GetChild(1).GetComponent<GridLayout>();

    }

    public void CreateFromTask(FoodTask task)
    {
        List<FoodItem> ingredients = task.GetIngredients();

        foreach (FoodItem item in ingredients)
        {
            Image image = GetImageForIngredient(item.Icon);
            
            image.transform.SetParent(_ingredientHolder.transform);
        }

        ReScaleGrid();
    }

    private Image GetImageForIngredient(Sprite sprite)
    {
        GameObject gameObject = new GameObject();

        Image image = gameObject.AddComponent<Image>();
        image.sprite = sprite;
        return image;
    }

    private void ReScaleGrid()
    {
        float width = _cellSize * _cellSpacingX * _ingredientHolder.transform.childCount + _foodHolder.preferredWidth + 25f;
        _bodyRect.sizeDelta = new Vector2(width, _bodyRect.sizeDelta.y);
    }
}
