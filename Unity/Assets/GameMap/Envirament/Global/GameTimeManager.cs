using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;

public class GameTimeManager : MonoBehaviour
{
    [Header("Time settings")]
    [Range(0, 1440f)]
    public float currentTime;
    public int DayPassed=0;
    public int CurrentHour;
    public int CurrentHours;
    public int CurrentMinute;
    public static float timeSpeed= 1f;

    [Header("Sun Settings")]
    public Light SunLight;
    [Range(0, 90f)]
    public float SunLatitude = 20f;
    [Range(-180, 180f)]
    public float SunLongitude = -90f;
    public float sunIntensity = 1f;
    public AnimationCurve sunIntensityMultiplier;
    public AnimationCurve SunTempetureCurve;

    [Header("Cycle")]
    public bool isDay = true;
    public bool SunActive = true;
    public bool MoonActive = true;

    [Header("Moon Settings")]
    public Light MoonLight;
    [Range(0, 90f)]
    public float MoonLatitude = 40f; //For better night sky the star and the moon long latitute must be the same
    [Range(-180, 180f)]
    public float MoonLongitude = 90f;
    public float MoonIntensity = 1f;
    public AnimationCurve MoonIntensityMultiplier;
    public AnimationCurve MoonTempetureCurve;

    [Header("Underwater")]
    public Light Underwater;

    [Header("Stars")]
    public VolumeProfile volumeProfile;
    private PhysicallyBasedSky skySettings;
    public float StarIntensity = 1f;
    public AnimationCurve starsCurve;
    [Range(0, 90f)]
    public float PolarStarLatitude = 40f; //For better night sky the star and the moon long latitute must be the same
    [Range(-180, 180f)]
    public float PolarStarLongitude = 90f;

    [Header("Objects")]
    public GameObject NightFogObj;
    private NightFog NightFogs;
    private bool NightMood=false;

    [Header("Sessions")]
    private int Season;
    private int SeasonDay=1;
    private float SunGoDown=18f*60f;
    private float SunGoUp=5f*60f;
    private float MoonGoDown=4.3f*60f;
    private float MoonGoUp= 18.3f * 60f;
    private float StarGoDown;
    private float StarGoUp;

    // Start is called before the first frame update
    void Start()
    {
        //int hourtimerandom = UnityEngine.Random.Range(17, 18);
        int hourtimerandom = UnityEngine.Random.Range(7, 15);
        int mintimerandom = UnityEngine.Random.Range(0, 60);
        currentTime = hourtimerandom * 60 + mintimerandom;
        Season= UnityEngine.Random.Range(1, 4);
        SeasonDay = UnityEngine.Random.Range(1, 4);
        NightFogs = NightFogObj.GetComponent<NightFog>();
        Underwater = transform.Find("Underwater").gameObject.GetComponent<Light>();
        Seasons();
        UpdateTimeText();
        CheckShadowStatus();
        SkyStar();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateTimeText();
        UpdateLight();
        CheckShadowStatus();
        SkyStar();
    }

    private void OnValidate()
    {
        UpdateLight();
        CheckShadowStatus();
        SkyStar();
    }

    private void UpdateTimeText()
    {
        currentTime += Time.deltaTime * timeSpeed;

        if (currentTime >= 1440)
        {
            currentTime = 0;
            DayPassed++;
            SeasonDay++;
            if (SeasonDay==5)
            {
                Season++;
                SeasonDay = 1;
                if (Season == 5)
                {
                    Season = 1;
                }
            }
            Seasons();
        }
        CurrentHour = (int)(currentTime / 60);
        CurrentHours= (int)(currentTime / 60);
        CurrentMinute = (int)(currentTime-CurrentHour*60);

        if (MoonActive && !SunActive && !NightMood && currentTime >= SunGoDown && currentTime < SunGoDown + 20f)
        {
            NightFogObj.SetActive(true);
            NightFog nightFogInstance = NightFogObj.GetComponent<NightFog>();
            if (nightFogInstance != null)
            {
                StartCoroutine(nightFogInstance.FogInTheNight(50f, true));
            }
            NightMood = true;
        }
        if (NightMood)
        {
            if (MoveControl.UnderwaterEffects)
            {
                NightFogObj.SetActive(false);
            }
            else
            {
                NightFogObj.SetActive(true);
            }
        }
        if (!MoonActive && SunActive && NightMood && currentTime>=SunGoUp && currentTime < SunGoUp+20f)
        {
            NightFog nightFogInstance = NightFogObj.GetComponent<NightFog>();
            if (nightFogInstance != null)
            {
                StartCoroutine(nightFogInstance.FogInTheNight(50f, false));
            }
            NightMood = false;
        }
    }

    private void UpdateLight()
    {
        float sunRotation = currentTime / 1440f * 360;
        SunLight.transform.localRotation = (Quaternion.Euler(SunLatitude - 90, SunLongitude, 0) * Quaternion.Euler(0, sunRotation, 0));
        MoonLight.transform.localRotation = (Quaternion.Euler(90 - MoonLatitude, SunLongitude, 0) * Quaternion.Euler(0, sunRotation, 0));

        float normalizedTime = currentTime / 1440f;
        float SunIntensitycurve = sunIntensityMultiplier.Evaluate(normalizedTime);
        float MoonIntensitycurve = MoonIntensityMultiplier.Evaluate(normalizedTime);

        HDAdditionalLightData SunlightData = SunLight.GetComponent<HDAdditionalLightData>();
        HDAdditionalLightData MoonlightData = MoonLight.GetComponent<HDAdditionalLightData>();


        if (SunlightData != null)
        {
            SunlightData.intensity = SunIntensitycurve * sunIntensity;
        }
        if (MoonlightData != null)
        {
            MoonlightData.intensity = MoonIntensitycurve * MoonIntensity;
        }

        float SuntempetureMultipler = SunTempetureCurve.Evaluate(normalizedTime);
        Light SunlightComponent = SunLight.GetComponent<Light>();

        float MoontempetureMultipler = MoonTempetureCurve.Evaluate(normalizedTime);
        Light MoonlightComponent = SunLight.GetComponent<Light>();

        if (SunlightComponent != null)
        {
            SunlightComponent.colorTemperature = SuntempetureMultipler * 2000f;
        }
        if (MoonlightComponent != null)
        {
            MoonlightComponent.colorTemperature = MoontempetureMultipler * 13000f;
        }
    }

    void CheckShadowStatus()
    {
        HDAdditionalLightData sunLightData = SunLight.GetComponent<HDAdditionalLightData>();
        HDAdditionalLightData MoonlightData = MoonLight.GetComponent<HDAdditionalLightData>();
        HDAdditionalLightData UnderwaterData=Underwater.GetComponent<HDAdditionalLightData>();
        float currentSunRation = currentTime;
        if (!MoveControl.UnderwaterEffects)
        {
            Underwater.gameObject.SetActive(false);
            UnderwaterData.EnableShadows(false);
            if (currentSunRation >= SunGoUp && currentSunRation <= SunGoDown)
            {
                sunLightData.EnableShadows(true);
                MoonlightData.EnableShadows(false);
                isDay = true;
            }
            else
            {
                sunLightData.EnableShadows(false);
                MoonlightData.EnableShadows(true);
                isDay = false;
            }

            if (currentSunRation >= SunGoUp && currentSunRation <= SunGoDown)
            {
                SunLight.gameObject.SetActive(true);
                SunActive = true;
            }
            else
            {
                SunLight.gameObject.SetActive(false);
                SunActive = false;
            }

            if (currentSunRation <= MoonGoDown || currentSunRation >= MoonGoUp)
            {
                MoonLight.gameObject.SetActive(true);
                MoonActive = true;
            }
            else
            {
                MoonLight.gameObject.SetActive(false);
                MoonActive = false;
            }
        }
        else {
            MoonActive = false;
            SunActive = false;
            MoonLight.gameObject.SetActive(false);
            SunLight.gameObject.SetActive(false);
            Underwater.gameObject.SetActive(true);
            sunLightData.EnableShadows(false);
            MoonlightData.EnableShadows(false);
            UnderwaterData.EnableShadows(true);
        }


    }

    private void SkyStar()
    {
        volumeProfile.TryGet<PhysicallyBasedSky>(out skySettings);
        skySettings.spaceEmissionMultiplier.value = starsCurve.Evaluate(currentTime / 1440f) * StarIntensity * 2;

        skySettings.spaceRotation.value = (Quaternion.Euler(90 - PolarStarLatitude, PolarStarLongitude, 0) * Quaternion.Euler(0, currentTime / 1440.0f * 360.0f, 0)).eulerAngles;
    }

    private void Seasons()
    {
        if (Season == 1 && Season == 3)
        {
            SunGoUp = UnityEngine.Random.Range(4f * 60f, 5.5f * 60f);
            SunGoDown = UnityEngine.Random.Range(18.5f * 60f, 19.5f * 60f);
        }
        else if (Season == 2)
        {
            SunGoUp = UnityEngine.Random.Range(4f * 60f, 5.5f * 60f);
            SunGoDown = UnityEngine.Random.Range(20f * 60f, 21.7f * 60f);
        }
        else if (Season == 4)
        {
            SunGoUp = UnityEngine.Random.Range(5.7f * 60f, 7.5f * 60f);
            SunGoDown = UnityEngine.Random.Range(20f * 60f, 21.7f * 60f);
        }
        MoonGoUp = SunGoDown - UnityEngine.Random.Range(0.2f * 60f, 0.5f * 60f);
        MoonGoDown = SunGoUp - Random.Range(0.2f * 60f, 0.5f * 60f);

        //StarGoUp = MoonGoUp + UnityEngine.Random.Range(0.7f * 60f, 3 * 60f);
        //StarGoDown = MoonGoDown - Random.Range(1f * 60f, 3 * 60f);
    }
}
