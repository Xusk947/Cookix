using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IconGenerator : MonoBehaviour
{
    [SerializeField]
    private BurgerItem _burger;
    [SerializeField]
    private FoodItem _cutlet, _cheese, _latooc;
    private Image _image;
    private float _ticker;
    void Start()
    {
        _image = transform.GetComponentInChildren<Image>();
    }

    void Update()
    {
        _ticker -= Time.deltaTime;
        if (_ticker < 0)
        {
            if (_image.sprite != null && _image.sprite.name == "G")
            {
                Destroy(_image.sprite.texture);
            }
            FoodEntity burger = _burger.Create();
            {
                FoodEntity burgerNew = CombineShit(burger);
                Destroy(burger);
                burger = burgerNew;
            }
            _ticker = 1f;
            Destroy(burger.gameObject);
            _image.sprite = ScreenshotManager.Instance.TakeFoodEntityScreenShot(burger);
        }
    }

    private FoodEntity CombineShit(FoodEntity foodEntity)
    {
        FoodEntity combined1, combined2;
        foodEntity.CanCombine(_cutlet.Create(), out combined1);
        FoodEntity created = GetFoodEntity();
        foodEntity.CanCombine(created, out combined2);
        Destroy(foodEntity.gameObject);
        Destroy(created.gameObject);
        Destroy(combined1.gameObject);
        return combined2;
    }

    private FoodEntity GetFoodEntity()
    {
        float v = Random.Range(0, 2);
        if (v < 1f) return _cheese.Create();
        if (v < 2f) return _latooc.Create();
        return _latooc.Create();
    }
}
