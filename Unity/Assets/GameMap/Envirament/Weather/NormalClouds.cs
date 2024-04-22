using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;

public class NormalClouds : MonoBehaviour
{
    [Header("Object")]
    private Volume Volume;
    private VolumetricClouds Clouds;
    private PhysicallyBasedSky PhySky;
    private VisualEnvironment VisualEnvironments;

    [Header("Effects")]
    private GameObject Effects;
    public EffectsScript EffectsScripts;

    [Header("Wind")]
    private float WindSpeed;
    private float NewWindSpeed;
    private float WindOrientation;
    private float NewWindOrientation;

    [Header("Clouds")]
    private float transitionTimer = 0f;
    private float fadeTimer;

    [Header("Used-Clouds-Settings")]
    private float CloudDensityMultiplier;
    private float ShapeFactor;
    private float ErosionFactor;
    private float AltitudeRange;
    private float BottomAltitude;
    private Vector3 Offset;
    private float SunLightDimmer;
    private float AmbientLightProbeDimmer;

    [Header("Clouds-New-Values")]
    private float NewdensityMultiplier;
    private float NewShapeFacor;
    private float NewErosionFactor;
    private Vector3 NewOffset;
    private float NewBottomAltitude;
    private float NewAltitudeRange;
    private float NewSunLightDimmer;
    private float NewAmbientLightProbeDimmer;


    [Header("Rain")]
    private bool Rain=false;
    private bool Thunder = false;
    private bool Lightning = false;
    private float endtime;

    private void Start()
    {
        Volume = GetComponent<Volume>();
        Volume.profile.TryGet<VisualEnvironment>(out VisualEnvironments);
        Volume.profile.TryGet<PhysicallyBasedSky>(out PhySky);
        Volume.profile.TryGet<VolumetricClouds>(out Clouds);
        PhySky.groundTint.showAlpha = false;
        Effects = EffectsScripts.gameObject;
        Offset=new Vector3(0,0,0);
        NewCloudSettings();
        ReturnUsedValues();
    }

    public IEnumerator CloudUpdate()
    {
        Clouds.scatteringTint.value = new Color(72f/255f, 65f / 255f, 65f / 255f);
        NewCloudSettings();
        transitionTimer = 0f;
        fadeTimer = Random.Range(115f,118f);
        while (transitionTimer < fadeTimer)
        {
            transitionTimer += Time.deltaTime * GameTimeManager.timeSpeed;
            float t = transitionTimer / fadeTimer;
            CloudChange(t);
            yield return null;
        }
        ReturnUsedValues();
    }

    private void NewCloudSettings()
        {
            NewdensityMultiplier=Random.Range(0f, 0.6f);
            NewBottomAltitude = Random.Range(0.01f, 2f);

            NewShapeFacor = Random.Range(0.6f, 1f);

            NewErosionFactor=Random.Range(0.6f, 1f);

            NewOffset = Offset+new Vector3(Random.Range(-5f, 5f), 0, 0);

            NewAltitudeRange = Random.Range(2200f, 6200f);

            NewSunLightDimmer = Random.Range(0f, 1f);
            NewAmbientLightProbeDimmer = Random.Range(0f, 1f);

            NewWindSpeed = Random.Range(WindSpeed - 20f, WindSpeed + 20f);
            NewWindOrientation= Random.Range(WindSpeed - 200f, WindSpeed + 200f);
            if ((100f < NewWindSpeed) || (NewWindSpeed < 0f))
            {
                NewWindSpeed = Random.Range(1f, 51f);
            }
            if ((360f < NewWindOrientation) || (NewWindOrientation > 0f))
            {
            NewWindOrientation = Random.Range(0f, 100f);
            }
    }

    private void ReturnUsedValues()
    {
            CloudDensityMultiplier = NewdensityMultiplier;
            BottomAltitude = NewBottomAltitude;

            ErosionFactor = NewErosionFactor;

            ShapeFactor = NewShapeFacor;

            Offset = NewOffset;

            AltitudeRange = NewAltitudeRange;

            SunLightDimmer = NewSunLightDimmer;
            AmbientLightProbeDimmer = NewAmbientLightProbeDimmer;

            WindSpeed = NewWindSpeed;
            WindOrientation = NewWindOrientation;
    }

    private void CloudChange(float t) 
    {
        //Wind Change
        VisualEnvironments.windOrientation.value = Mathf.Lerp(WindOrientation, NewWindOrientation, t);
        VisualEnvironments.windSpeed.value = Mathf.Lerp(WindSpeed, NewWindSpeed, t);

        //Clouds Change
        Clouds.densityMultiplier.value = Mathf.Lerp(CloudDensityMultiplier, NewdensityMultiplier, t);
        Clouds.bottomAltitude.value = Mathf.Lerp(BottomAltitude, NewBottomAltitude, t);
        
        Clouds.shapeFactor.value = Mathf.Lerp(ShapeFactor, NewShapeFacor, t);

        Clouds.shapeOffset.value= Vector3.Lerp(Offset, NewOffset, t);

        Clouds.erosionFactor.value = Mathf.Lerp(ErosionFactor, NewErosionFactor, t);
        Clouds.altitudeRange.value = Mathf.Lerp(AltitudeRange, NewAltitudeRange, t);
        Clouds.sunLightDimmer.value = Mathf.Lerp(SunLightDimmer, NewSunLightDimmer, t);
        Clouds.ambientLightProbeDimmer.value = Mathf.Lerp(AmbientLightProbeDimmer, NewAmbientLightProbeDimmer, t);
    }

    public IEnumerator StormChange()
    {
        Clouds.scatteringTint.value = new Color(0f / 255f, 0f / 255f, 0f / 255f);
        PhySky.groundTint.showAlpha = true;
        EffectsScripts.ActiveEffects = true;
        StormSettings();
        transitionTimer = 0f;
        endtime = 0f;
        float StormStart = 0f;
        float StormTime = 0f;
        float StormEnd = 0f;
        float StormType = Random.Range(0f, 20f);
        if ( (StormType >5) && (StormType < 13) )
        {
            StormStart = Random.Range(10f, 20f);
            StormTime = Random.Range(60f, 360f);
            StormEnd = Random.Range(10f, 20f);
            Rain = true;
            EffectsScripts.JustRain();
            Debug.Log("Just Rain");
        }
        else if (StormType <= 5)
        {
            StormStart = Random.Range(10f, 20f);
            StormTime = Random.Range(100f, 460f);
            StormEnd = Random.Range(10f, 20f);
            Thunder = true;
            EffectsScripts.JustThunder();
            Debug.Log("Just Thunder");
        }
        else if ((StormType >= 13) && (StormType < 17))
        {
            StormStart = Random.Range(30f, 120f);
            StormTime = Random.Range(180f, 360f);
            StormEnd = Random.Range(20f, 120f);
            Rain = true;
            Thunder = true;
            Lightning= true;
            Debug.Log("Thunderstorm");
            EffectsScripts.Thunderstorm();
            //Thunderstorm
        }
        else
        {
            StormStart = Random.Range(30f, 120f);
            StormTime = Random.Range(430f, 660f);
            StormEnd = Random.Range(20f, 120f);
            Rain = true;
            Thunder = true;
            Lightning = true;
            //StormAndThunder
            Debug.Log("A really good Storm");
            EffectsScripts.StormAndThunder();
        }
        float Storm=StormStart+StormTime+StormEnd;
        
        while (transitionTimer < Storm)
        {
            transitionTimer += Time.deltaTime * GameTimeManager.timeSpeed;
            if(transitionTimer <= StormStart)
            {
                float t = transitionTimer / StormStart;
                CloudChange(t);
                if (t>=0.5f)
                {
                    float EffectsTime = (t - 0.5f) * 2f;
                    if (Rain)
                    {
                        EffectsScripts.RainChanges(true, EffectsTime);
                    }
                    if (Thunder)
                    {

                    }
                    if (Lightning)
                    {

                    }
                    
                }
            }
            else if(transitionTimer > StormStart && transitionTimer < Storm-StormEnd)
            {
                if (MoveControl.UnderwaterEffects)
                {
                    EffectsScripts.ActiveEffects = false;
                    EffectsScripts.RainParticleSystem.gameObject.SetActive(false);
                }
                else
                {
                    EffectsScripts.ActiveEffects = true;
                    if (Rain)
                    {
                        EffectsScripts.RainParticleSystem.gameObject.SetActive(true);
                    }
                }
            }
            else if(transitionTimer >= Storm - StormEnd)
            {
                endtime += Time.deltaTime * GameTimeManager.timeSpeed;
                NewCloudSettings();
                float t = endtime / StormEnd;
                CloudChange(t);
                if (t <= 0.5f)
                {
                    float EffectsTime = t * 2f;
                    if (Rain)
                    {
                        EffectsScripts.RainChanges(false,EffectsTime);
                    }
                    if (Thunder)
                    {

                    }
                    if (Lightning)
                    {

                    }

                }
            }
            yield return null;
        }

        ReturnUsedValues();
        PhySky.groundTint.showAlpha = false;
        Rain = false;Thunder = false; Lightning = false;
        EffectsScripts.ActiveEffects = false;
        EffectsScripts.EffectAllow();
        transform.parent.parent.gameObject.GetComponent<GameEventInterface>().StormIsActive = false;
    }

    private void StormSettings()
        {
            NewdensityMultiplier = Random.Range(0.8f, 1f);
            NewBottomAltitude = Random.Range(0.01f,1f);

            NewShapeFacor = Random.Range(0.0f, 0.3f);

            NewErosionFactor = Random.Range(0.0f, 0.3f);

            NewOffset = Offset+new Vector3(Random.Range(-8f, 5f), 0, 0);

            NewAltitudeRange = Random.Range(800f, 2200f);

            NewSunLightDimmer = Random.Range(0.183f, 0.5f);
            NewAmbientLightProbeDimmer = Random.Range(0.183f, 0.5f);


            NewWindSpeed = Random.Range(WindSpeed - 20f, WindSpeed + 20f);
            NewWindOrientation = Random.Range(WindSpeed - 200f, WindSpeed + 200f);
            if ((100f < NewWindSpeed) || (NewWindSpeed < 0f))
            {
                NewWindSpeed = Random.Range(1f, 51f);
            }
            if ((360f < NewWindOrientation) || (NewWindOrientation > 0f))
            {
                NewWindOrientation = Random.Range(0f, 100f);
            }
        }

}
