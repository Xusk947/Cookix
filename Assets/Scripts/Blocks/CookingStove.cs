using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingStove : Table
{
    public KitchenItem ItemToSpawn;
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
        if (itemEntity == null) return;
        if (itemEntity is not KitchenItemEntity) return;
        KitchenItemEntity kitchenItemEntity = itemEntity as KitchenItemEntity;
        if (kitchenItemEntity.CanCook())
        {
            hud.SetActive(true);
            kitchenItemEntity.cookingProgress += Time.deltaTime / 10f;
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
        hud.transform.localPosition = new Vector3(0, 0, 0.02f);
        progressBar = hud.transform.GetChild(1).GetComponent<Image>();
        progressBar.fillAmount = 0;
        hud.SetActive(false);
    }
}
