using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class GameEventInterface : MonoBehaviour
{
    [Header("Times")]
    public Waterbehavior Waterbehavior;
    private GameTimeManager GameTimeManagers;

    [Header("Weather Scripts")]
    private FogScript FogScripts;
    private NormalClouds NormalClouds;

    [Header("Gameobjects")]
    [HideInInspector] private GameObject Ocean;

    [Header("Counter")]
    private int daycounter = 0;
    private int hourcounter = 0;

    [Header("Storm")]
    [HideInInspector] public bool StormIsActive;

    [Header("Fog")]
    public Volume FogVolume;
    public bool FogActive;
    private bool CreateFog;
    private double Fogstart;

    [Header("Longitude")]
    private float Sunlongitude;
    private float Moonlongitude;
    private bool Switchside=false;

    [Header("Clouds")]
    private Volume DefaultClouds;

    private GameObject globals;

    private void Locations()
    {
        if (Sunlongitude > 90 || Moonlongitude > 90)
        {
            Switchside = true;
        }
        else if (Switchside && (5>Sunlongitude ||5>Moonlongitude) )
        {
            Switchside = false;
        }

        if (Switchside)
        {
            Moonlongitude = Moonlongitude - Random.Range(1f, 5f);
            Sunlongitude = Sunlongitude - Random.Range(1f, 5f);
        }
        else
        {
            Sunlongitude = Sunlongitude + Random.Range(1f, 5f);
            Moonlongitude = Moonlongitude + Random.Range(1f, 5f);
        }

        // Example: Randomize sun longitude
        GameTimeManagers.SunLongitude = Sunlongitude;

        // Example: Randomize moon longitude
        GameTimeManagers.MoonLongitude = Moonlongitude;
    }

    private void Start()
    {
        // Find the GameTimeManager object in the scene
        SearchObject();
        Sunlongitude = Random.Range(0f, 90f);
        Moonlongitude = Random.Range(0f, 90f);
        Locations();
    }

    private void SearchObject()
    {
        FogVolume.gameObject.SetActive(false);
        GameTimeManagers = GetComponent<GameTimeManager>();
        Ocean = Waterbehavior.gameObject;
        hourcounter = GameTimeManagers.CurrentHour;
        FogScripts=FogVolume.GetComponent<FogScript>();
        globals = transform.Find("Global Volumes").gameObject;
        DefaultClouds = globals.transform.Find("Clouds").GetComponent<Volume>();
        NormalClouds = DefaultClouds.GetComponent<NormalClouds>();
    }

    private void Update()
    {
        if (daycounter != GameTimeManagers.DayPassed)
        {
            ChangeEverthing();
            daycounter = GameTimeManagers.DayPassed;
        }

        if (MoveControl.UnderwaterEffects)
        {
            FogScripts.gameObject.SetActive(false);
            NormalClouds.gameObject.SetActive(false);
        }
        else
        {
            if (FogActive){FogScripts.gameObject.SetActive(true);}
            NormalClouds.gameObject.SetActive(true);
        }

        WeatherUpdate();
        if(hourcounter != GameTimeManagers.CurrentHours)
        {
            Waterbehavior.RandomizedWaterChanges(true);
            hourcounter= GameTimeManagers.CurrentHour;
            if (!MoveControl.UnderwaterEffects)
            {
                if (!StormIsActive)
                {
                    float stat = Random.value;
                    if (stat > 0.1f)
                    {
                        StartCoroutine(NormalClouds.CloudUpdate());
                    }
                    else
                    {
                        StormIsActive = true;
                        StartCoroutine(NormalClouds.StormChange());
                    }
                }
            }

        }

    }

    private void ChangeEverthing()
    {
        Locations();
        FogChance();
    }

    //Fog Methodes
    private void WeatherUpdate()
    {
        if (CreateFog && GameTimeManagers.CurrentHour == Fogstart)
        {
            FogActive= true;
            FogVolume.gameObject.SetActive(true);
            StartCoroutine(FogScripts.NormalFog());
        }
    }

    private void FogChance()
    {
        float stat = Random.value;
        if (stat > 0.3f)
        {
            CreateFog = false;
        }
        else
        {
            CreateFog = true;
            Fogstart = Random.Range(4 * 60, 7 * 60);
        }
    }

}
