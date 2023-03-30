
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlicingTable : Table
{
    public float SliceTime = 0.125f;
    // UI
    private GameObject _hud;
    private Image _progressBar;

    // Slice Group
    private GameObject _knife;
    private bool _isSlicing = false;
    private float _sliceProgress = 0f;
    private float _sliceTimer = 0f;
    /// <summary>
    /// Player who slicing on this block
    /// At the same time only 1 player can slice Items
    /// </summary>
    private PlayerController _player;

    protected new void Start()
    {
        base.Start();
        SpawnHUD();
        _knife = transform.Find("knife").gameObject;
    }

    protected void FixedUpdate()
    {
        // Prevent Block Animation from Slicing while player doesn't close to this block
        if (!_isSlicing) return;
        _sliceTimer -= Time.deltaTime;
        if (_sliceTimer < 0f)
        {
            Slice();
            if (_sliceProgress >= 1f)
            {
                FinishSlice(Item as FoodEntity);
            }
        }
    }

    public override void Interact(PlayerController player)
    {
        if (_sliceProgress > 0f) return;
        base.Interact(player);
    }

    public override void SecondInteract(PlayerController player, bool isPress)
    {
        if (_itemEntity == null) return;
        if (_itemEntity is not FoodEntity) return;
        FoodEntity foodEntity = _itemEntity as FoodEntity;
        if (!foodEntity.foodItem.canBeSliced) return;
        if (player.CurrentItem != null) return;
        _isSlicing = isPress;
        if (!isPress)
        {
            StopSlicing();
            return;
        }
        _player = player;
        _player.IsSlicing = true;
    }

    private void FinishSlice(FoodEntity foodEntity)
    {
        
        StopSlicing();
        _sliceProgress = 0f;
        FoodEntity slicedFoodEntity = foodEntity.foodItem.slicedPrefab.Create();
        Destroy(foodEntity.gameObject);
        Item = slicedFoodEntity;
    }

    private void StopSlicing()
    {
        _progressBar.fillAmount = 0f;
        _hud.SetActive(false);
        _knife.SetActive(true);
        _isSlicing = false;

        if (_player != null)
        {
            _player.IsSlicing = false;
        }
    }

    private void Slice()
    {
        _sliceTimer = SliceTime;
        _sliceProgress += 0.1f;
        _progressBar.fillAmount = _sliceProgress;
        _hud.SetActive(true);
        _knife.SetActive(false);
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

    public override void BlockHover(bool focused)
    {
        base.BlockHover(focused);
        if (!focused)
        {
            StopSlicing();
        }
    }
}
