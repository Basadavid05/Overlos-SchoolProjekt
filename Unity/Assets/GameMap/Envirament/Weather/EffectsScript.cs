using UnityEngine;

public class EffectsScript : MonoBehaviour
{
    [Header("Effects")]
    private GameObject player;
    public bool ActiveEffects=false;

    [Header("WeatherEffects")]
    private GameObject RainObj;
    private GameObject LightningObj;

    [Header("Control")]
    public MoveControl Control;

    [Header("RainSpeed")]
    public ParticleSystem RainParticleSystem;
    private ParticleSystem.EmissionModule RainEmission;
    private ParticleSystem.ShapeModule RainShape;

    private Vector3 rainposition;
    private Vector3 rainvelocity;



    public enum Raintype
    {
        Light=17,
        Normal=77,
        Fast=177,
    }

    private Raintype RainSpeed;

    private bool RainRain;


    private void Start()
    {
        Components();
        Activetes();
    }

    private void Components()
    {
        player=Control.gameObject;
        transform.gameObject.SetActive(true);
        RainSpeed = Raintype.Light;
        RainParticleSystem.gameObject.transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        RainEmission = RainParticleSystem.emission;
        RainShape = RainParticleSystem.shape;
        rainposition = RainParticleSystem.transform.position;
        rainvelocity = new Vector3(player.transform.position.x,0,player.transform.position.y);
        RainObj = RainParticleSystem.gameObject;
        LightningObj = transform.Find("Lightning").gameObject;
    }


    private void Activetes()
    {
        RainObj.SetActive(false);
    }

    public void EffectAllow()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    private void LateUpdate()
    {
        if (!MoveControl.UnderwaterEffects && ActiveEffects)
        {
            if(RainRain)
            {
                Vector3 playerMovement=new Vector3(player.transform.position.x,0,player.transform.position.z);
                /* if (rainvelocity.x - playerMovement.x > 10f)
                {
                    RainShape.position = new Vector3(rainvelocity.x - playerMovement.x, 0, 0);
                }*/
                RainShape.position = playerMovement;
            }

        }
    }

    public void JustRain()
    {
        RainDefault();
        RainSpeed = Raintype.Light;
    }

    public void JustThunder()
    {
        
    }

    public void Thunderstorm()
    {
        RainDefault();
        RainSpeed = Raintype.Normal;
        LightningObj.SetActive(true);
    }

    public void StormAndThunder()
    {
        RainDefault();
        RainSpeed = Raintype.Fast;
        LightningObj.SetActive(true);
    }

    private void RainDefault()
    {
        RainObj.SetActive(true);
        RainEmission.rateOverTime = 140;
        RainRain = true;
    }


    public void RainChanges(bool reverse, float t)
    {
        float TargetSpeed=0f;
        float MinRate=0f;
        float targetRate;

        if (reverse){TargetSpeed = (int)RainSpeed;}
        else{MinRate = (int)RainSpeed;}

        targetRate = Mathf.Lerp(MinRate, TargetSpeed,t);

        if (Mathf.Approximately(targetRate,TargetSpeed))
        { targetRate = TargetSpeed; }
        RainEmission.rateOverTime = targetRate;
    }

}
