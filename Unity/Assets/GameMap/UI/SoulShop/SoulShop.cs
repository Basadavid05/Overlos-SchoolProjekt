using UnityEngine;
using TMPro;

public class SoulShop : MonoBehaviour
{

    [Header("Items")]
    public static bool SoulShopActive=false;
    public int SoulShopPaying;
    public Item[] SoulObjects;

    [Header("SoulShop")]
    public Transform SoulContent;
    public GameObject SoulContainer;
    public SoulCounterController SoulCounterController;

    [Header("Status")]
    private GameObject Canvas;
    private GameObject PlayerUI;

    [Header("Pages")]
    public GameObject Page1;
    public GameObject Page2;
    public GameObject button1;
    public GameObject button2;

    [Header("Online Activation")]
    public TMP_InputField InputField;
    public GameObject AlertObj;

    [Header("Shop")]
    public GameObject ViewPort;
    public GameObject CartText;
    private Transform Cart1;
    private Transform Cart2;


    bool rm;

    private void Start()
    {
        Canvas = transform.GetChild(0).gameObject;
        PlayerUI = transform.parent.Find("Player-UI New").gameObject;
        Switcher(false);
        SoulShopPaying = 0;
        for (int i = 0; i < SoulObjects.Length; i++)
        {
            AddToShop(SoulObjects[i]);
        }
            Cart1 = ViewPort.transform.GetChild(0);
            Cart2 = ViewPort.transform.GetChild(1);
        
    }

    private void AddToShop(Item item)
    {
            GameObject obj = Instantiate(SoulContainer, SoulContent);
            var ItemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var ItemIcon = obj.transform.Find("InventoryItem").GetComponent<UnityEngine.UI.Image>();
            ItemName.text = item.ItemName+"   $"+item.SoulPrice;
            ItemIcon.sprite = item.Icon;
            obj.GetComponent<SoulShopItem>().SoulShopObj = this;
            obj.GetComponent<SoulShopItem>().item = item;
    }

    public void Switcher(bool status)
    {
        SoulShopActive = status;
        Canvas.SetActive(status);
        PlayerUI.SetActive(!status);
        Main.main.Lock(status);
        if (status)
        {
            Buttonchange(true);
        }
    }

    private void Update()
    {
        if (SoulShopActive)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Off();
            }
        }

        if (PlayerDatas.Death)
        {
            DeselectItems();
            SoulShopActive = false;
            Canvas.SetActive(false);
            PlayerUI.SetActive(true);
        }
    }

    public void Off()
    {
        Switcher(false);
        DeselectItems();
    }

    public void Buttonchange(bool status)
    {
            Page1.SetActive(status);
            button1.SetActive(status);

            Page2.SetActive(!status);
            button2.SetActive(!status);

        if (!status)
        {
            DeselectItems();
        }
        else
        {
            InputField.text = "";
        }
    }

    public void CheckTheTexts()
    {
        string enteredText = InputField.text.Trim();
        if (enteredText != "")
        {
            bool foundMatch = false;

            foreach (Item item in SoulObjects)
            {
                if (enteredText == item.Code)
                {
                    foundMatch = true;
                    Debug.Log("Congratulations! Match found.");
                    ItemPlacement.instance.Add(item);
                    GameObject alert = Instantiate(AlertObj, Page2.transform);
                    alert.GetComponent<AlertObj>().CodeActivation(item);
                    InputField.text = "";
                    return;
                }
            }

            if (!foundMatch)
            {
            
                GameObject alert = Instantiate(AlertObj, Page2.transform);
                alert.GetComponent<AlertObj>().NotGood(false);
            }
        }
    }

    public void Pay()
    {
        if (SoulShopPaying != 0)
        {
            if((SoulCounterController.SoulCount - SoulShopPaying) > 0)
            {
                SoulCounterController.SoulCount = SoulCounterController.SoulCount - SoulShopPaying;
                SoulCounterController.UpdateCounting();
                AddItemsToInventory();
                DeselectItems();
            }
            else
            {
                GameObject Alert = Instantiate(AlertObj, Page1.transform);
                Alert.GetComponent<AlertObj>().NotGood(true);
            }
        }
    }

    public void AddToBasketText(string name,int price)
    {
        GameObject obj1 = Instantiate(CartText, Cart1);
        GameObject obj2 = Instantiate(CartText, Cart2);
        obj1.GetComponent<TextMeshProUGUI>().text = name;
        obj2.GetComponent<TextMeshProUGUI>().text = "$"+price;
    }

    public void DestroyBasketItem(string name,int index)
    {
        UpdateIndex(index);
        Destroy(Cart1.GetChild(index).gameObject);
        Destroy(Cart2.GetChild(index).gameObject);
    }

    private void UpdateIndex(int index)
    {
        for (int i = 0; i < SoulContent.childCount; i++)
        {
            if (SoulContent.GetChild(i).GetComponent<SoulShopItem>().ItemIndex > index)
            {
                SoulContent.GetChild(i).GetComponent<SoulShopItem>().ItemIndex--;
            }
        }
    }

    private void DeselectItems()
    {
        for (int i = 0; i < SoulContent.childCount; i++)
        {
            SoulContent.GetChild(i).GetComponent<SoulShopItem>().DeselectItem();
        }
        for (int i = 0; i < Cart1.childCount; i++)
        {
            Destroy(Cart1.GetChild(i).gameObject);
            Destroy(Cart2.GetChild(i).gameObject);
        }
    }

    public int NextItemPLace()
    {
        int x = Cart1.childCount;
        return x;
    }

    private void AddItemsToInventory()
    {
        for (int i = 0; i < SoulContent.childCount; i++)
        {
            if (SoulContent.GetChild(i).GetComponent<SoulShopItem>().clicked)
            {
                ItemPlacement.instance.Add(SoulContent.GetChild(i).GetComponent<SoulShopItem>().item);
            }
        }
    }

}
