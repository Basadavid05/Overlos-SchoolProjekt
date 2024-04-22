using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;

public class NightFog : MonoBehaviour
{
    [Header("Object")]
    private Fog fogObject;
    private Volume Volume;
    // private PhysicallyBasedSky PhysicallyBasedSkys;

    void Start()
    {
        Volume = GetComponent<Volume>();
        //Volume.profile.TryGet<PhysicallyBasedSky>(out PhysicallyBasedSkys);
        Volume.profile.TryGet<Fog>(out fogObject);
        if (Volume.profile.TryGet<Fog>(out fogObject))
        {
            Volume.weight = 0f;
        }

    }

    private void NightFogValues()
    {
        Volume.profile.TryGet<Fog>(out fogObject);
        fogObject.tint.value = new Color(24f / 255f, 22f / 255f, 22f / 255f);
        fogObject.albedo.value = new Color(7f / 255f, 7f / 255f, 7f / 255f);

        fogObject.meanFreePath.value = Random.Range(7f, 24f);
        fogObject.maximumHeight.value = Random.Range(720f, 1313f);
        fogObject.volumetricFogBudget = Random.Range(1000f, 1300f);
    }

    public IEnumerator FogInTheNight(float timeBeing, bool SwitchSides)
    {
        float FogTransitionTimer = 0f;
        if (SwitchSides)
        {
            Volume = GetComponent<Volume>();
            NightFogValues();
            while (FogTransitionTimer < timeBeing)
            {
                FogTransitionTimer += Time.deltaTime * GameTimeManager.timeSpeed;
                float t;
                t = FogTransitionTimer / timeBeing;
                Volume.weight = t;
                yield return null;
            }
        }
        else
        {
            Volume = GetComponent<Volume>();
            while (FogTransitionTimer < timeBeing)
            {
                FogTransitionTimer += Time.deltaTime * GameTimeManager.timeSpeed;
                float t = 1 - (FogTransitionTimer / timeBeing);
                if (Volume == null)
                {
                    yield break;
                }
                Volume.weight = t;
                yield return null;
            }
        }
        
        if (!SwitchSides) { gameObject.SetActive(false); }
    }

}
