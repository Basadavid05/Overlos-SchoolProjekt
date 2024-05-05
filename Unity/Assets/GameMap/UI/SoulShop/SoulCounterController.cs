using System.Collections;
using TMPro;
using UnityEngine;

public class SoulCounterController : MonoBehaviour
{
    private GameObject SoulCounter;

    [Header("Souls")]
    private int SoulCounting=-1;
    public static int SoulCounts = 0;

    [Header("Text")]
    private TextMeshProUGUI text;
    public TextMeshProUGUI SoulShopCounter;
    private float fadeDuration = 5.0f;
    private int spawn;

    [Header("Count")]
    private bool activeagain=false;
    private bool isCountingDown = false;
    private int Counting= 0;

    // Start is called before the first frame update
    void Start()
    {
        if (spawn == 0)
        {
            SoulCounts = 0;
            spawn = 1;
        }
        SoulCounter = transform.GetChild(0).gameObject;
        text= SoulCounter.transform.Find("Count").GetComponent<TextMeshProUGUI>();
        SoulCounter.SetActive(false);
        text.text = SoulCounts + "";
    }

    IEnumerator ShowCanvasForDuration()
    {
        SoulCounter.SetActive(true);

        // Wait for 5 seconds
        yield return new WaitForSeconds(fadeDuration);
        if (Counting==1)
        {
            SoulCounter.SetActive(false);
            isCountingDown = false;
            Counting = 0;
        }
        else
        {
            Counting--;
        }

    }

    // Update is called once per frame
    private void Update()
    {
        if (!PauseMenu.GameIsPause && !SoulShop.SoulShopActive && !MapControl.MapIsEnabled)
        {
            if (InventoryController.InventoryOpen)
            {
                SoulCounter.SetActive(true);
                activeagain = true;
                isCountingDown = true;

            }
            else
            {
                if (activeagain && isCountingDown)
                {
                    Counting = 1;
                    StartCoroutine(ShowCanvasForDuration());
                    activeagain = false;
                }
            }
        }
        else
        {
            SoulCounter.SetActive(false);
            activeagain = true;
            isCountingDown = true;
        }

        if (SoulCounts > SoulCounting)
        {
            UpdateCounting();
            Counting++;
            isCountingDown = true;
            StartCoroutine(ShowCanvasForDuration());

        }
    }


    public void UpdateCounting()
    {

        SoulCounting = SoulCounts;
        text.text = SoulCounts + "";
        SoulShopCounter.text = SoulCounts + "";
    }
}

