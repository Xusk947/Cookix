
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlicingTable : Table
{
    private GameObject _hud;
    private Image _progressBar;

    private Animator _animator;
    private bool _isSlicing = false;
    private float _sliceProgress = 0f;

    protected new void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
        _animator.StartPlayback();
        SpawnHUD();
    }

    public override void Interact(PlayerController player)
    {
        if (_sliceProgress > 0f) return;
        base.Interact(player);
    }

    public override void SecondInteract(PlayerController player)
    {
        if (_itemEntity == null) return;
        print("Item Entity not equal to null");
        if (_itemEntity is not FoodEntity) return;
        print("it's a FoodEntity");
        FoodEntity foodEntity = _itemEntity as FoodEntity;
        if (!foodEntity.foodItem.canBeSliced) return;
        print("Item can be sliced");
        _sliceProgress += 0.1f;
        _progressBar.fillAmount = _sliceProgress;
        _hud.SetActive(true);

        if (_sliceProgress >= 1f)
        {
            _hud.SetActive(false);
            _progressBar.fillAmount = 0f;
            _sliceProgress = 0f;
            FoodEntity slicedFoodEntity = foodEntity.foodItem.slicedPrefab.Create();
            Destroy(foodEntity.gameObject);
            Item = slicedFoodEntity;
        }
    }

    private void SpawnHUD()
    {
        _hud = Instantiate(Content.Instance.CookProgressBar);
        _hud.transform.SetParent(transform);
        _hud.transform.localPosition = new Vector3(0, 1f, 0.02f);
        _progressBar = _hud.transform.GetChild(1).GetComponent<Image>();
        _progressBar.fillAmount = 0;
        _hud.SetActive(false);
    }
}
