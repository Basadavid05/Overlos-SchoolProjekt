using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightningColor : MonoBehaviour
{
    private ParticleSystem ParticleSystem;
    private List<Color> colors = new List<Color>();

    private void RandomColor()
    {
        colors.Add(Color.white);
        colors.Add(Color.blue);
        colors.Add(Color.red);
        colors.Add(Color.cyan);
        colors.Add(Color.grey);
        colors.Add(Color.magenta);

        int randomIndex = Random.Range(0, colors.Count);
        var mainModule = ParticleSystem.main;
        mainModule.startColor = colors[randomIndex];
    }

    /* private void OnEnable()
    {
        ParticleSystem.emission.SetBurst(0, new ParticleSystem.Burst(0, 1));
        ParticleSystem.emission.SetBurst(1, new ParticleSystem.Burst(1, 1));
        ParticleSystem.emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0, 1), new ParticleSystem.Burst(1, 1) });
        ParticleSystem.emission.SetBurst(0, new ParticleSystem.Burst(0, 1));
        ParticleSystem.emission.SetBurst(1, new ParticleSystem.Burst(1, 1));
        ParticleSystem.emission.SetBurst(2, new ParticleSystem.Burst(2, 1));
        ParticleSystem.emission.SetBurst(3, new ParticleSystem.Burst(3, 1));
        ParticleSystem.emission.SetBurst(4, new ParticleSystem.Burst(4, 1));
        ParticleSystem.emission.SetBurst(5, new ParticleSystem.Burst(5, 1));
        ParticleSystem.emission.SetBurst(6, new ParticleSystem.Burst(6, 1));
        ParticleSystem.emission.SetBurst(7, new ParticleSystem.Burst(7, 1));
        ParticleSystem.emission.SetBurst(8, new ParticleSystem.Burst(8, 1));
        ParticleSystem.emission.SetBurst(9, new ParticleSystem.Burst(9, 1));
        ParticleSystem.emission.SetBurst(10, new ParticleSystem.Burst(10, 1));
        ParticleSystem.emission.SetBurst(11, new ParticleSystem.Burst(11, 1));
        ParticleSystem.emission.SetBurst(12, new ParticleSystem.Burst(12, 1));
        ParticleSystem.emission.SetBurst(13, new ParticleSystem.Burst(13, 1));
        ParticleSystem.emission.SetBurst(14, new ParticleSystem.Burst(14, 1));
        ParticleSystem.emission.SetBurst(15, new ParticleSystem.Burst(15, 1));
        ParticleSystem.emission.SetBurst(16, new ParticleSystem.Burst(16, 1));
        ParticleSystem.emission.SetBurst(17, new ParticleSystem.Burst(17, 1));
        ParticleSystem.emission.SetBurst(18, new ParticleSystem.Burst(18, 1));
        ParticleSystem.emission.SetBurst(19, new ParticleSystem.Burst(19, 1));
    }*/

    private IEnumerator ChangeColorRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => ParticleSystem.isEmitting); // Wait until particles are emitting
            RandomColor();
            yield return new WaitForSeconds(0.2f); // Change color every 1 second (adjust as needed)
        }
    }

    private void Start()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
        if (ParticleSystem == null)
        {
            Debug.LogError("Particle System component not found on the GameObject.");
        }
        else
        {
            StartCoroutine(ChangeColorRoutine());
        }
    }
}
