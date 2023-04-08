using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Cooking Stove a child of block which is used for cooking items with special kind of KitchenItem
/// </summary>
public class CookingStove : Table
{
    /// <summary>
    /// Which Item should spawn on this Block when a game is start
    /// </summary>
    public KitchenItem ItemToSpawn;
    /// <summary>
    /// is a GameObject of Cooking Process and Warning Sign Info
    /// </summary>
    private GameObject hud;
    /// <summary>
    /// using to display current cook progress
    /// </summary>
    private Image progressBar;
    /// <summary>
    /// speed with in block will cook items on it
    /// </summary>
    private float _cookSpeed = .5f;
    protected new void Start()
    {
        base.Start();
        // Spawn Kitchen Item on it
        if (ItemToSpawn != null)
        {
            SpawnKitchenItemEntity(ItemToSpawn);
        }
        // Create a HUD instance
        SpawnHUD();
    }
    void Update()
    {
        // Hide HUD by default and check for _itemEntity on it
        hud.SetActive(false);
        // Check item for null and default class and later cast to a Kitchen ItemEntity
        KitchenItemEntity kitchenItemEntity = CheckAndGetKitchenItemEntity(_itemEntity);
        if (kitchenItemEntity == null) return;
        if (kitchenItemEntity.CanCook())
        {
            hud.SetActive(true);
            kitchenItemEntity.cookingProgress += Time.deltaTime * _cookSpeed * GameManager.Instance.rules.CookingStoveCookSpeedMultiplayer;
            progressBar.fillAmount = kitchenItemEntity.cookingProgress;
            if (kitchenItemEntity.cookingProgress > 1.0f)
            {
                kitchenItemEntity.CookItemsInside();
            }
        };
    }
    /// <summary>
    /// Check Item Entity if it exist and is an Kitchen Item Entity and return casted Item Entity
    /// If object isn't KitchenItemEntity return null
    /// </summary>
    /// <param name="item">Item for check and cast to KitchenItemEntity</param>
    /// <returns></returns>
    private KitchenItemEntity CheckAndGetKitchenItemEntity(ItemEntity item)
    {
        if (_itemEntity == null) return null;
        if (_itemEntity is not KitchenItemEntity) return null;
        return _itemEntity as KitchenItemEntity;
    }
    /// <summary>
    /// Create an Item
    /// </summary>
    /// <param name="Instance"></param>
    private void SpawnKitchenItemEntity(KitchenItem Instance)
    {
        Item = Instance.Create();
    }
    /// <summary>
    /// Spawn HUD which is a part of cooking progress bar
    /// </summary>
    private void SpawnHUD()
    {
        hud = Instantiate(Content.Instance.CookProgressBar);
        hud.transform.SetParent(transform);
        hud.transform.localPosition = new Vector3(0, 1f, 0.02f);
        progressBar = hud.transform.GetChild(1).GetComponent<Image>();
        progressBar.fillAmount = 0;
        hud.SetActive(false);
    }
}
