using UnityEngine;
using UnityEngine.UI;


public class SoulShopItem : MonoBehaviour
{
    private Image backgroundv2;
    private Color v1;
    private Color v2=Color.red;

    public bool clicked;
    public SoulShop SoulShopObj;
    public Item item;
    public int ItemIndex=-1;

    private void Start()
    {
        backgroundv2 = GetComponent<Image>();
        v1=backgroundv2.color;
        backgroundv2.color=v2;
    }

    public void Click()
    {
        if (clicked)
        {
            DeselectItem();

        }
        else
        {
            backgroundv2.color = v1;
            clicked = true;
            SoulShopObj.SoulShopPaying = SoulShopObj.SoulShopPaying + item.SoulPrice;
            ItemIndex = SoulShopObj.NextItemPLace();
            SoulShopObj.AddToBasketText(item.ItemName, item.SoulPrice);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!SoulShop.SoulShopActive && clicked)
        {
            backgroundv2.color = v2;
            clicked = false;
        }
    }

    public void DeselectItem()
    {
        if (ItemIndex > -1)
        {
            backgroundv2.color = v2;
            clicked = false;
            SoulShopObj.SoulShopPaying = SoulShopObj.SoulShopPaying - item.SoulPrice;
            SoulShopObj.DestroyBasketItem(item.ItemName, ItemIndex);
            ItemIndex = -1;
        }
    }
}
