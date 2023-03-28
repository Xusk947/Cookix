using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingStove : Table
{
    public KitchenItem ItemToSpawn;
    public float cookSpeed = .5f;
    private GameObject hud;
    private Image progressBar;
    protected new void Start()
    {
        base.Start();
        if (ItemToSpawn != null)
        {
            SpawnKitchenItemEntity(ItemToSpawn);
        }
        SpawnHUD();
    }
    void Update()
    {
        hud.SetActive(false);
        if (_itemEntity == null) return;
        if (_itemEntity is not KitchenItemEntity) return;
        KitchenItemEntity kitchenItemEntity = _itemEntity as KitchenItemEntity;
        if (kitchenItemEntity.CanCook())
        {
            hud.SetActive(true);
            kitchenItemEntity.cookingProgress += Time.deltaTime * cookSpeed;
            progressBar.fillAmount = kitchenItemEntity.cookingProgress;
            if (kitchenItemEntity.cookingProgress > 1.0f)
            {
                kitchenItemEntity.CookItemsInside();
            }
        };
    }

    private void SpawnKitchenItemEntity(KitchenItem Instance)
    {
        Item = Instance.Create();
    }

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
