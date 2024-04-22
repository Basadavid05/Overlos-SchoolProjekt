using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDatas : MonoBehaviour
{
    public float PlayerHeight = 2f;

    [Header("Health")]
    public int PlayerCurrentHealth;
    public static int PlayerHP;
    public static bool Death=false;
    public static int PlayerMaxHealth = 700;
    public Slider sliders;
    public GameObject Resurect;


    public float minTime = 60*8f;
    public float maxTime = 60*10f;

    [Header("Lerp-Health")]
    public float chipspeed = 10f;

    private void Start()
    {
        Death = false;
        GetComponent<Rigidbody>().freezeRotation = true;
        sliders.maxValue= PlayerMaxHealth;
        PlayerCurrentHealth = PlayerMaxHealth;
        PlayerHP = PlayerMaxHealth;
        StartCoroutine(AddSouls());

    }

    // Update is called once per frame
    private void Update()
    {
        if (PlayerHP!= PlayerCurrentHealth)
        {
            if(PlayerHP > PlayerCurrentHealth)
            {
                PlayerCurrentHealth=PlayerHP;
                StartCoroutine(AnimateSliderValue(PlayerHP, chipspeed));
            }
            else if(PlayerHP < PlayerCurrentHealth)
            {
                PlayerCurrentHealth = PlayerHP;
                StartCoroutine(AnimateSliderValue(PlayerHP, chipspeed));
                if(PlayerHP <= 0)
                {
                    sliders.value = 0;
                    Time.timeScale = 0f;
                    Main.main.Lock(true);
                    ItemPlacement.instance.Death();
                    Resurect.SetActive(true);
                    Death = true;
                }
            }
        }
    }


    public static void Takedamage(int damage)
    {
        PlayerHP -= damage;
    }

    public static void RestoreHealth(int restoreHealth)
    {
        if (PlayerHP + restoreHealth > PlayerMaxHealth)
        {
            PlayerHP = PlayerMaxHealth;
        }
        else
        {
            PlayerHP += restoreHealth;
        }
    }

    private IEnumerator AnimateSliderValue(float targetHealth, float duration)
    {
        float startHealth = sliders.value;
        float timer = 0f;

        while (timer < duration)
        {
            float progress = timer / duration;
            float currentHealth = Mathf.Lerp(startHealth, targetHealth, progress);
            sliders.value = currentHealth;
            timer += Time.deltaTime;
            yield return null;
        }

        sliders.value = targetHealth; // Ensure we reach the exact target value
    }


    IEnumerator AddSouls()
    {
        while (true)
        {
            // Wait for a random time between minTime and maxTime
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);
            SoulCounterController.SoulCount = SoulCounterController.SoulCount + 50;
        }
    }

    /*
     * public void updateHealthUI()
    {
        Debug.Log(PlayerCurrentHealth);
        float fillFront = frontheader.fillAmount;
        float fillBackground = backHeader.fillAmount;
        float hFraction = PlayerCurrentHealth / PlayerMaxHealth;
        if (fillBackground > hFraction)
        {
            backHeader.color = Color.red;
            frontheader.fillAmount = 0.5f;
            lerpTimer += Time.deltaTime;
            float percentComplete=lerpTimer/chipspeed;
            percentComplete = percentComplete * percentComplete;
            backHeader.fillAmount = Mathf.Lerp(fillBackground, hFraction, .002f);
        }
        if (fillFront > hFraction)
        {
            backHeader.color = Color.green;
            backHeader.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipspeed;
            percentComplete = percentComplete * percentComplete;
            frontheader.fillAmount = Mathf.Lerp(fillFront, backHeader.fillAmount, 2f);
        }

        }*/

}



     
 
 