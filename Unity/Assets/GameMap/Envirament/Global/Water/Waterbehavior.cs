using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using System.Collections;

public class Waterbehavior : MonoBehaviour
{
    private WaterSurface WaterSurface; 

    // Reference to the HDRP water material         
    // public Material waterMaterial;

    private float transitionDuration;

    private float transitionTimer = 0f;

    [Header("Old-Water")]     // Store the initial settings
    private float initialRipplesChaos;
    private float initialLargeChaos;
    private float initialLargeWindSpeed;
    private float initialRipplesWindSpeed;
    private float initialRepetitionSize;
    private float initialSimulationFoamAmount;
    private float initialSimulationFoamSmoothness;
    private float initialLargeBand1Multiplier;
    private float initialLargeBand0Multiplier;
    private float initialLargeBand0FadeDistance;
    private float initialLargeBand1FadeDistance;
    private float CurrentSpeed;
    private float CurrentOrientation;

    [Header("New-Water")] // Store the initial settings for the new properties
    private float newInitialRipplesChaos;
    private float newInitialLargeChaos;
    private float newInitialLargeWindSpeed;
    private float newInitialRipplesWindSpeed;
    private float newInitialRepetitionSize;
    private float newInitialSimulationFoamAmount;
    private float newInitialSimulationFoamSmoothness;
    private float newInitialLargeBand1Multiplier;
    private float newInitialLargeBand0Multiplier;
    private float newInitialLargeBand0FadeDistance;
    private float newInitialLargeBand1FadeDistance;
    private float NewCurrentSpeed;
    private float NewCurrentOrientation;



    private void Start()
    {
        WaterSurface= GetComponent<WaterSurface>();
        RandomizedWaterChanges(false);
        WaterPast();
    }

    public void RandomizedWaterChanges(bool state) 
        //if RandomizedWaterChanges is false then the script run in the start, if true then run in an update scripts 
    {
        if (state)
        {
            // Determine the current hour in the game time
            transitionDuration=Random.Range(55,59);
            NewWaterProperty();
            StartCoroutine(TransitionSettings());

        }
        else
        {
            // Randomize water color
            WaterSurface.ripplesChaos = Random.Range(0f, 1f);
            WaterSurface.largeChaos = Random.Range(0f, 1f);

            // Randomize water tiling and offset
            WaterSurface.largeWindSpeed = Random.Range(0f, 151f);
            WaterSurface.ripplesWindSpeed = Random.Range(0f, 16f);

            WaterSurface.repetitionSize = Random.Range(350f, 2001f);

            WaterSurface.simulationFoamAmount = Random.Range(0f, 0.51f);
            WaterSurface.simulationFoamSmoothness = Random.Range(0f, 0.51f);
            WaterSurface.largeCurrentSpeedValue = Random.Range(-5f, 5);
            WaterSurface.largeCurrentOrientationValue = Random.Range(-5f,5);
            WaterSurface.largeBand1Multiplier = Random.Range(0f, 1f);
            WaterSurface.largeBand0Multiplier = Random.Range(0f, 1f);
            WaterSurface.largeBand0FadeDistance = Random.Range(0f, 1f);
            WaterSurface.largeBand1FadeDistance = Random.Range(0f, 1f);
        }
        
    }


    IEnumerator TransitionSettings()
    {
        transitionTimer = 0f;

        while (transitionTimer < transitionDuration)
        {
            transitionTimer += Time.deltaTime*GameTimeManager.timeSpeed;

            float t = transitionTimer / transitionDuration;
            SetInterpolatedWaterProperties(t);

            yield return null;
        }
        WaterPast();
    }

    private void SetInterpolatedWaterProperties(float t)
    {
        WaterSurface.ripplesChaos = Mathf.Lerp(initialRipplesChaos, newInitialRipplesChaos, t);
        WaterSurface.largeChaos = Mathf.Lerp(initialLargeChaos, newInitialLargeChaos, t);
        WaterSurface.largeWindSpeed = Mathf.Lerp(initialLargeWindSpeed, newInitialLargeWindSpeed, t);
        WaterSurface.ripplesWindSpeed = Mathf.Lerp(initialRipplesWindSpeed, newInitialRipplesWindSpeed, t);
        WaterSurface.repetitionSize = Mathf.Lerp(initialRepetitionSize, newInitialRepetitionSize, t);
        WaterSurface.simulationFoamAmount = Mathf.Lerp(initialSimulationFoamAmount, newInitialSimulationFoamAmount, t);
        WaterSurface.simulationFoamSmoothness = Mathf.Lerp(initialSimulationFoamSmoothness, newInitialSimulationFoamSmoothness, t);
        WaterSurface.largeBand1Multiplier = Mathf.Lerp(initialLargeBand1Multiplier, newInitialLargeBand1Multiplier, t);
        WaterSurface.largeBand0Multiplier = Mathf.Lerp(initialLargeBand0Multiplier, newInitialLargeBand0Multiplier, t);
        WaterSurface.largeBand0FadeDistance = Mathf.Lerp(initialLargeBand0FadeDistance, newInitialLargeBand0FadeDistance, t);
        WaterSurface.largeBand1FadeDistance = Mathf.Lerp(initialLargeBand1FadeDistance, newInitialLargeBand1FadeDistance, t);
        WaterSurface.largeCurrentOrientationValue = Mathf.Lerp(CurrentOrientation, NewCurrentOrientation, t);
        WaterSurface.largeBand1FadeDistance = Mathf.Lerp(CurrentSpeed, NewCurrentSpeed, t);
    }
    
    private void NewWaterProperty()
    {
        newInitialRipplesChaos = Random.Range(0f, 1f);
        newInitialLargeChaos = Random.Range(0f, 1f);
        newInitialLargeWindSpeed = Random.Range(0f, 151f);
        newInitialRipplesWindSpeed = Random.Range(0f, 16f);
        newInitialRepetitionSize = Random.Range(350f, 1701f);
        newInitialSimulationFoamAmount = Random.Range(0f, 0.51f);
        newInitialSimulationFoamSmoothness = Random.Range(0f, 0.51f);
        newInitialLargeBand1Multiplier = Random.Range(0f, 1f);
        newInitialLargeBand0Multiplier = Random.Range(0f, 1f);
        newInitialLargeBand0FadeDistance = Random.Range(0f, 1f);
        newInitialLargeBand1FadeDistance = Random.Range(0f, 1f);
        NewCurrentOrientation=NewCurrentOrientation+ Random.Range(-5f, 5f);
        NewCurrentSpeed= NewCurrentSpeed + Random.Range(-5f, 5f);
    }

    private void WaterPast()
    {
        initialRipplesChaos = WaterSurface.ripplesChaos;
        initialLargeChaos = WaterSurface.largeChaos;
        initialLargeWindSpeed = WaterSurface.largeWindSpeed;
        initialRipplesWindSpeed = WaterSurface.ripplesWindSpeed;
        initialRepetitionSize = WaterSurface.repetitionSize;
        initialSimulationFoamAmount = WaterSurface.simulationFoamAmount;
        initialSimulationFoamSmoothness = WaterSurface.simulationFoamSmoothness;
        initialLargeBand1Multiplier = WaterSurface.largeBand1Multiplier;
        initialLargeBand0Multiplier = WaterSurface.largeBand0Multiplier;
        initialLargeBand0FadeDistance = WaterSurface.largeBand0FadeDistance;
        initialLargeBand1FadeDistance = WaterSurface.largeBand1FadeDistance;
        CurrentOrientation = WaterSurface.largeCurrentOrientationValue;
        CurrentSpeed = WaterSurface.largeCurrentSpeedValue;
    }

}
