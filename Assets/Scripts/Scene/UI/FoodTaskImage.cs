using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodTaskImage : MonoBehaviour
{
    private static float DEFAULT_HEIGHT = 100f;
    [SerializeField]
    private Image _bodyImage;
    [SerializeField]
    private Image _foodHolder;
    [SerializeField]
    private GridLayoutGroup _ingredientHolder;

    private RectTransform _bodyRect;

    private float _cellSize, _cellSpacingX;

    private FoodTask _task;

    public void Awake()
    {
        _bodyImage = transform.GetChild(0).GetComponent<Image>();
        _bodyRect = transform.GetChild(0).GetComponent<RectTransform>();
        _bodyImage.preserveAspect = false;

        _foodHolder = _bodyImage.transform.GetChild(0).GetComponent<Image>();

        _ingredientHolder = _bodyImage.transform.GetChild(1).GetComponent<GridLayoutGroup>();

        _cellSize = _ingredientHolder.cellSize.x;
        _cellSpacingX = _ingredientHolder.spacing.x;
    }

    public void CreateFromTask(FoodTask task)
    {
        List<FoodItem> ingredients = task.GetIngredients();
        FoodEntity fe = task.GetFoodEntity();
        _foodHolder.sprite = ScreenshotManager.Instance.TakeFoodEntityScreenShot(fe, true);

        foreach (FoodItem item in ingredients)
        {
            Image image = GetImageForIngredient(item.Icon);
            image.name = item.Name;
            image.transform.SetParent(_ingredientHolder.transform);
        }

        ReScaleGrid();
        UpdateParentPositionAndAnchors(_bodyRect);
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
        float width = (_cellSize + _cellSpacingX) * _ingredientHolder.transform.childCount + _foodHolder.rectTransform.sizeDelta.x + _foodHolder.rectTransform.position.x;
        print(width);
        _bodyRect.sizeDelta = new Vector2(width, DEFAULT_HEIGHT);
    }

    private void UpdateParentPositionAndAnchors(RectTransform rectTransform)
    {
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0f, rectTransform.rect.width);
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, rectTransform.rect.height);
        rectTransform.anchorMin = new Vector2(0f, 1f);
        rectTransform.anchorMax = new Vector2(0f, 1f);
    }
}
