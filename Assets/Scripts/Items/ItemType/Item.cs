using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Cookix/Items/Item")]
public class Item : ScriptableObject
{
    public GameObject Prefab;
    public Sprite Icon;
    public string Name;

    public ItemEntity Create()
    {
        ItemEntity itemEntity = Instantiate(Prefab).AddComponent<ItemEntity>();
        itemEntity.item = this;
        if (Icon != null)
        {
            GameObject icon = GenerateIcon();
            icon.transform.SetParent(itemEntity.transform);
            icon.transform.localScale = new Vector3(0, 0.5f, 0);
        }
        return itemEntity;
    }

    public GameObject GenerateIcon()
    {
        GameObject Canvas = Instantiate(Content.Instance.FoodItemUI);
        Canvas.transform.GetComponentInChildren<UnityEngine.UI.Image>().sprite = Icon;

        return Canvas;
    }
}
