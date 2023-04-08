using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodTaskImage : MonoBehaviour
{
    [SerializeField]
    private Gradient _gradient;
    private GridLayoutGroup _ingredientsGridLayoutGroup;
    private Image _foodTaskBackgroundImage;
    private Image _foodTaskImage;
    
    public void Awake()
    {
        Transform verticalLayoutGroup = transform.GetChild(0);

        _ingredientsGridLayoutGroup = verticalLayoutGroup.GetChild(0).GetComponent<GridLayoutGroup>();
        _foodTaskBackgroundImage = verticalLayoutGroup.GetChild(1).GetComponent<Image>();
        _foodTaskImage = _foodTaskBackgroundImage.transform.GetChild(0).GetComponent<Image>();

    }

    public void UpdateBackground(float value)
    {
        _foodTaskBackgroundImage.fillAmount = value;
        _foodTaskBackgroundImage.color = _gradient.Evaluate(value);
    }

    public void CreateFromTask(FoodTask task)
    {
        ClearLastTask();
        List<FoodItem> ingredients = task.GetIngredients();
        FoodEntity fe = task.GetFoodEntity();
        _foodTaskImage.sprite = ScreenshotManager.Instance.TakeFoodEntityScreenShot(fe, true);

        foreach (FoodItem item in ingredients)
        {
            Image image = GetImageForIngredient(item.Icon);
            image.name = item.Name;
            image.transform.SetParent(_ingredientsGridLayoutGroup.transform);
        }
    }

    private void ClearLastTask()
    {
        if (_ingredientsGridLayoutGroup.transform.childCount > 0)
        {
            for(int i = 0; i < _ingredientsGridLayoutGroup.transform.childCount; i++)
            {
                Destroy(_ingredientsGridLayoutGroup.transform.GetChild(i).gameObject);
            }
        }
    }

    private Image GetImageForIngredient(Sprite sprite)
    {
        GameObject gameObject = new GameObject();

        Image image = gameObject.AddComponent<Image>();
        image.sprite = sprite;
        image.name = "FoodIcon";

        Outline outline = gameObject.AddComponent<Outline>();
        outline.effectDistance = new Vector2(0.02f, 0.02f);
        outline.effectColor = Color.gray;

        gameObject.AddComponent<LookToCamera>();

        return image;
    }
}
