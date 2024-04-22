using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class FogScript : MonoBehaviour
{
    [Header("Object")]
    private Fog fogObject;
    private PhysicallyBasedSky PhysicallyBasedSkys;
    private Volume Volume;

    [Header("Fog")]
    public static bool CreateFog;
    private float Fogend;
    private float FogTransitionTimer = 0f;

    [Header("Values")]
    private float MaxDensityV;
    private float MaxBaseHeight;
    private float MaxFogDistance;
    private float MaxVolumetricVolume;

    void Start()
    {
        Volume = GetComponent<Volume>();
        Volume.profile.TryGet<PhysicallyBasedSky>(out PhysicallyBasedSkys);
        if (Volume.profile.TryGet<Fog>(out fogObject))
        {
            Volume.weight = 0f;
        }
    }

    public IEnumerator NormalFog()
    {
        fogObject.tint.value = new Color(191f / 255f, 191f / 255f, 191f / 255f, 1f);
        fogObject.albedo.value = new Color(191f / 255f, 191f / 255f, 191f / 255f, 1f);
        FogTransitionTimer = 0f;
        TimeSetting();
        RandomFogValues();
        while (FogTransitionTimer < Fogend)
            {
            FogTransitionTimer += Time.deltaTime * GameTimeManager.timeSpeed;
            
            float t = FogTransitionTimer / Fogend;

            if (t <= 1f / 4f)
            {
                // Transition from minimum to maximum
                float t1 = t / (1f / 4f);
                Volume.weight=t1;
            }
            else if (t <= 2f / 4f){}
            else
            {
                // Transition from maximum to minimum
                float t2 = (t - 2f / 4f) / (1f / 4f);
                Volume.weight=1f - t2; // Invert t2 as we are decreasing from maximum
            }

            yield return null;
            }
        transform.parent.GetComponent<GameEventInterface>().FogActive = false;
        gameObject.SetActive(false);
    }

    private void TimeSetting()
    {
        float randomValue = Random.value;

        if (randomValue < 0.8f)
        {  // 80% chance
            Fogend = Random.Range(0, 5);
        }
        else
        {
            Fogend = Random.Range(5, 11);  
        }
        Fogend = Fogend * 60;
    }

    private void RandomFogValues()
    {
        MaxDensityV = Random.Range(70f, 161f);
        MaxBaseHeight = Random.Range(300f, 401f);
        MaxFogDistance = Random.Range(250f, 401f);
        MaxVolumetricVolume = Random.Range(0f, 71f);

        fogObject.meanFreePath.value = MaxDensityV;
        fogObject.baseHeight.value = MaxBaseHeight;
        fogObject.maximumHeight.value = MaxFogDistance;
        fogObject.volumetricFogBudget = MaxVolumetricVolume;
    }


}
