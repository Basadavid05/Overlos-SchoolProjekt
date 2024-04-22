using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertObj : MonoBehaviour
{
    private GameObject page1;
    private GameObject page2;
    private TextMeshProUGUI Page2Text;
    private Image background;


    private void Starts()
    {
        page1 = transform.GetChild(0).GetChild(0).gameObject;
        page2 = transform.GetChild(0).GetChild(1).gameObject;
        page1.SetActive(false);
        page2.SetActive(false);
    }

    public void NotGood(bool status)
    {
        Starts();
        Page2Text = page2.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        page2.SetActive(true);
        if (status)
        {
            Page2Text.text = "You dont have enugh souls.";
        }
        else
        {
            Page2Text.text = "There are no code like this.";
        }
    }

    public void CodeActivation(Item item)
    {
        Starts();
        page1.SetActive(true);
        background = page1.transform.Find("Background").GetComponent<Image>();
        Page2Text=page1.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Page2Text.text=item.ItemName+"";
        background.sprite = item.Icon;

    }

    public void DestroyAlertObj()
    {
        Destroy(transform.gameObject);
    }
}
